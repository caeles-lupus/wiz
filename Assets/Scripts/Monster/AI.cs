using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Entity;

// Указывает, что для данного скрипта необходим скрипт либо Entity, либо основанные на нём.
[RequireComponent(typeof(Entity))]

public class AI: MonoBehaviour
{
    [Header("Сущность")]
    public Entity entity;

    //TODO: чтобы можно было задавать стартовое направление.
    public enum dir
    {
        toLeft,
        toRight
    };

    [Header("Полёт")]
    /// <summary>
    /// True - если монстр летающий.
    /// </summary>
    public bool isFlying = false;
    
    [Header("Компаньон")]
    /// <summary>
    /// Режим компаньона
    /// </summary>
    public bool isCompanion = false;
    /// <summary>
    /// Friend.
    /// </summary>
    public GameObject Friend;
    /// <summary>
    /// Расстояние на котором он "видит" друга, за которым будет следовать.
    /// </summary>
    [Range(2, 10000)] public float DistanceOfFollow = 10f;

    [Header("Общие")]
    /// <summary>
    /// Стартовое направление.
    /// </summary>
    public dir StartDrirection;
    /// <summary>
    /// Скорость передвижения.
    /// </summary>
    public float Speed = 3.5f;
    /// <summary>
    /// Расстояние на котором он патрулирует.
    /// </summary>
    [Range(2, 10000)] public int DistanceOfPatrol = 10;
    /// <summary>
    /// Расстояние, на котором кончается агр.
    /// </summary>
    [Range(5, 10000)] public float DistanceOfArgession = 8;

    [Header("Характеристики")]

    /// <summary>
    /// 
    /// </summary>
    [Range(0.5f, 10000f)] public float AttackValue = 1f;


    /// <summary>
    /// Начальная точка.
    /// </summary>
    private Vector3 startPos;

    private bool chill = true;
    private bool angry;
    private bool goBack;
    private bool follow;

    bool MoveRight;

    private Animator anim;
    bool isAnimated = false;

    private void Start()
    {
        if (entity == null)
        {
            Debug.LogError("У объекта " + gameObject.name + " не привязан скрипт entity или его потомки к скрипту AI!");
        }
    }

    void Awake()
    {
        startPos = transform.position;
        MoveRight = StartDrirection == dir.toRight;

        anim = GetComponent<Animator>();
        if (anim != null)
        {
            isAnimated = true;
            // Т.к. патрулирование подразумевает под собой постоянное хождение.
            anim.SetBool("isRun", true);
        }
    }

    void Follow()
    {
        Turn(transform.position.x < Friend.transform.position.x);
        // Если летающий.
        if (isFlying)
        {
            // Определяем координату головы ГГ. TODO: не сработает для любого другого юнита.
            float FriendY = Friend.GetComponent<CapsuleCollider2D>().bounds.max.y + 0.5f;
            // Создаем новую коорду-цель.
            Vector2 target = new Vector2(Friend.transform.position.x, FriendY);
            // Перемещаемся на новую коорду.
            transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        }
        else
        {
            Vector2 target = new Vector2(Friend.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Обычное движение.
    /// </summary>
    void Chill()
    {
        if (transform.position.x > (startPos.x + DistanceOfPatrol))
        {
            Turn(false);
            MoveRight = false;
        }
        else if (transform.position.x < (startPos.x - DistanceOfPatrol))
        {
            Turn(true);
            MoveRight = true;
        }

        if (MoveRight)
        {
            transform.position = new Vector2(transform.position.x + Speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - Speed * Time.deltaTime, transform.position.y);
        }
    }
    /// <summary>
    /// Агр.
    /// </summary>
    void Angry()
    {
        Turn(transform.position.x < Hero.Instance.transform.position.x);
        if (isFlying)
        {
            transform.position = Vector2.MoveTowards(transform.position, Hero.Instance.transform.position, Speed * Time.deltaTime);
        }
        else
        {
            Vector2 target = new Vector2(Hero.Instance.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        }
    }
    /// <summary>
    /// Домой.
    /// </summary>
    void GoBack()
    {
        Turn(transform.position.x < startPos.x);
        if (isFlying)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos, Speed * Time.deltaTime);
        }
        else
        {
            Vector2 target = new Vector2(startPos.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (gameObject.name == "Pig 1")
        //{
        //    Debug.Log("11");
        //}
        
        if (entity.relation != Entity.Relation.AggressiveToAll && isCompanion)
        {
            if (Friend)
            {
                if (Vector2.Distance(transform.position, Friend.transform.position) < DistanceOfFollow)
                {
                    follow = true;
                    angry = false;
                    chill = false;
                    goBack = false;
                }
            }
        }

        // ToChill
        if (!chill && !angry && !follow && Mathf.Abs(transform.position.x - startPos.x) < DistanceOfPatrol && 
            (transform.position.y == startPos.y || !isFlying))
        {
            chill = true;
            goBack = false;
        }
        
        if (entity.relation == Entity.Relation.AggressiveToAll || entity.relation == Entity.Relation.AggressiveToPlayer &&
            Vector2.Distance(transform.position, Hero.Instance.transform.position) < DistanceOfArgession)
        {
            angry = true;
            chill = false;
            goBack = false;
        }

        if (angry && (Vector2.Distance(transform.position, Hero.Instance.transform.position) > DistanceOfArgession))
        {
            goBack = true;
            angry = false;
        }

        if (follow)
        {
            Follow();
        }
        else if (chill)
        {
            Chill();
        }
        else if (angry)
        {
            Angry();
        }
        else if (goBack)
        {
            GoBack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!TagsSets.tagsForAI.Contains(collision.gameObject.tag))
        {
            FlipX();
            MoveRight = !MoveRight;
        }
        else if (collision.gameObject == Hero.Instance.gameObject)
        {
            if (isAnimated)
            {
                //anim.SetBool("isRun", false);
                anim.SetTrigger("Attack");
            }
            attackEnemy(Hero.Instance.gameObject);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MovingRight"></param>
    private void Turn(bool MovingRight = false)
    {
        var x = Mathf.Abs(transform.localScale.x);

        if (MovingRight ^ StartDrirection == dir.toRight) //Xor
            transform.localScale = new Vector3(-x, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    /// <summary>
    /// 
    /// </summary>
    private void FlipX()
    {
        var x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void attackEnemy(GameObject target)
    {
        // Выходим, если этот объект не подходит для атаки (земля, монетки, кристаллы).
        if (TagsSets.tagsNonTarget.Contains(target.tag)) return;

        //
        if (entity.relation == Relation.AggressiveToAll)
        {
            //Decor dec = ListsOfObjects.GetDecorOfGameObject(collision.gameObject);
            //if (dec)
            //{
            //    dec.GetDamage();
            //}
            Monster monster = ListsOfObjects.GetMonsterOfGameObject(target);
            if (monster)
            {
                monster.GetDamage(AttackValue, gameObject);
            }
            //Obstacle obstacle = ListsOfObjects.GetObstacleOfGameObject(collision.gameObject);
            //if (obstacle)
            //{
            //    obstacle.GetDamage();
            //}
            Hero hero = Hero.Instance;
            if (target == hero.gameObject)
            {
                hero.GetDamage(AttackValue, gameObject);
                //inContact = true;
            }
        }
        else if (entity.relation == Relation.AggressiveToPlayer)
        {
            Hero hero = Hero.Instance;
            if (target == hero.gameObject)
            {
                hero.GetDamage(AttackValue, gameObject);
                //inContact = true;
            }
        }
        else if (entity.relation == Relation.FrendlyToPlayer)
        {
            //Decor dec = ListsOfObjects.GetDecorOfGameObject(collision.gameObject);
            //if (dec)
            //{
            //    dec.GetDamage();
            //}
            Monster monster = ListsOfObjects.GetMonsterOfGameObject(target);
            if (monster)
            {
                monster.GetDamage(AttackValue, gameObject);
            }
            //Obstacle obstacle = ListsOfObjects.GetObstacleOfGameObject(collision.gameObject);
            //if (obstacle)
            //{
            //    obstacle.GetDamage();
            //}
        }
    }


}
