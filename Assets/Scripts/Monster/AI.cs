using System.Collections.Generic;
using UnityEngine;

class ControllerAnimation
{
    Animator anim;
    bool isAnimated = false;
    public ControllerAnimation(Animator anim)
    {
        this.anim = anim;
        isAnimated = anim != null;
    }

    public void Relax()
    {
        if (isAnimated)
        {
            anim.SetBool("isRelax", true);
            anim.SetBool("isRun", false);
        }
    }

    public void Run()
    {
        if (isAnimated)
        {
            //anim.ResetTrigger("Attack");
            anim.SetBool("isRun", true);
        }
    }

    public void Attack()
    {
        if (isAnimated)
        {
            anim.SetBool("isRun", false);
            anim.SetTrigger("Attack");
        }
    }
}

// ���������, ��� ��� ������� ������� ��������� ������ ���� Entity, ���� ���������� �� ��.
[RequireComponent(typeof(Entity))]
public class AI: MonoBehaviour
{
    [Header("��������")]
    public Entity entity;

    public bool isCannotMove = false;

    //TODO: ����� ����� ���� �������� ��������� �����������.
    public enum dir
    {
        toLeft,
        toRight
    };

    [Header("����")]
    /// <summary>
    /// True - ���� ������ ��������.
    /// </summary>
    public bool isFlying = false;
    
    [Header("���������")]
    /// <summary>
    /// ����� ����������
    /// </summary>
    public bool isCompanion = false;
    /// <summary>
    /// Friend.
    /// </summary>
    public GameObject Friend;
    /// <summary>
    /// ���������� �� ������� �� "�����" �����, �� ������� ����� ���������.
    /// </summary>
    [Range(2, 10000)] public float DistanceOfFollow = 10f;

    [Header("�����")]
    /// <summary>
    /// ��������� �����������.
    /// </summary>
    public dir StartDrirection;
    /// <summary>
    /// �������� ������������.
    /// </summary>
    public float Speed = 3.5f;
    /// <summary>
    /// ���������� �� ������� �� �����������.
    /// </summary>
    [Range(2, 10000)] public int DistanceOfPatrol = 10;
    /// <summary>
    /// ����������, �� ������� ��������� ���.
    /// </summary>
    [Range(5, 10000)] public float DistanceOfArgession = 8;

    [Header("��������������")]

    /// <summary>
    /// 
    /// </summary>
    [Range(0.5f, 10000f)] public float AttackValue = 1f;
    /// <summary>
    /// 
    /// </summary>
    [Range(0.1f, 1000f)] public float AttackPeriod = 1f;

    /// <summary>
    /// ��������� �����.
    /// </summary>
    private Vector3 startPos;

    private bool chill = true;
    private bool angry;
    private bool goBack;
    private bool follow;

    bool MoveRight;

    //TEMP FIX:
    [Header("�������� :(")]
    public bool haveBullet = false;

    ControllerAnimation ani;

    private List<TargetAndItsTiming> targets;

    private void Start()
    {
        if (entity == null)
        {
            Debug.LogError("� ������� " + gameObject.name + " �� �������� ������ entity ��� ��� ������� � ������� AI!");
        }
        targets = new List<TargetAndItsTiming>();
        if (isCannotMove) ani.Relax();
    }

    void Awake()
    {
        startPos = transform.position;
        MoveRight = StartDrirection == dir.toRight;

        ani = new ControllerAnimation(GetComponent<Animator>());
        // �.�. �������������� ������������� ��� ����� ���������� ��������.
        ani.Run();
    }

    void Follow()
    {
        Turn(transform.position.x < Friend.transform.position.x);
        // ���� ��������.
        if (isFlying)
        {
            // ���������� ���������� ������ ��. TODO: �� ��������� ��� ������ ������� �����.
            float FriendY = Friend.GetComponent<CapsuleCollider2D>().bounds.max.y + 0.5f;
            // ������� ����� ������-����.
            Vector2 target = new Vector2(Friend.transform.position.x, FriendY);
            // ������������ �� ����� ������.
            transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        }
        else
        {
            Vector2 target = new Vector2(Friend.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// ������� ��������.
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
    /// ���.
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
    /// �����.
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
        if (isCannotMove)
        {
            return;
        }

        // ��������: ���������
        if (entity.relation != Relation.AggressiveToAll && isCompanion)
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
        
        //TEMP FIX
        if (!haveBullet)
        if (!goBack && entity.relation == Relation.AggressiveToAll || entity.relation == Relation.AggressiveToPlayer &&
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
            ani.Run();
            Follow();
        }
        else if (chill)
        {
            ani.Run();
            Chill();
        }
        else if (angry)
        {
            ani.Run();
            Angry();
        }
        else if (goBack)
        {
            ani.Run();
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
        // �������, ���� ���� ������ �� �������� ��� ����� (�����, �������, ���������).
        if (TagsSets.tagsNonTarget.Contains(collision.gameObject.tag)) return;
        // �������, ���� �� ������ �� �������.
        if (entity.relation == Relation.FrendlyToAll) return;

        //
        if (targets.Count > 0)
        {
            TargetAndItsTiming foundTarget = targets.Find(trg => trg.Target == collision.gameObject);
            if (foundTarget != null)
            {
                return;
            }
        }
        TargetAndItsTiming target = new TargetAndItsTiming(collision.gameObject, 0f);
        targets.Add(target);

        //��������
        if (haveBullet)
        {
            ani.Attack();
        }
        else
        {
            attackEnemy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (targets.Count > 0)
        {
            TargetAndItsTiming target = targets.Find(trg => trg.Target == collision.gameObject);
            if (target != null)
            {
                target.TimeAttack += Time.deltaTime;
                if (target.TimeAttack >= AttackPeriod)
                {
                    target.TimeAttack = 0;

                    if (haveBullet)
                    {
                        ani.Attack();
                    }
                    else
                    {
                        attackEnemy(collision.gameObject);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (targets.Count > 0)
        {
            TargetAndItsTiming target = targets.Find(trg => trg.Target == collision.gameObject);
            if (target != null)
            {
                targets.Remove(target);
            }
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
        // �������, ���� ���� ������ �� �������� ��� ����� (�����, �������, ���������).
        if (TagsSets.tagsNonTarget.Contains(target.tag)) return;

        //
        if (entity.relation == Relation.AggressiveToAll)
        {
            //Decor dec = ListsOfObjects.GetDecorOfGameObject(collision.gameObject);
            //if (dec)
            //{
            //    ani.Attack();
            //    dec.GetDamage();
            //}
            Monster monster = ListsOfObjects.GetMonsterOfGameObject(target);
            if (monster)
            {
                ani.Attack();
                monster.GetDamage(AttackValue, gameObject);
            }
            //Obstacle obstacle = ListsOfObjects.GetObstacleOfGameObject(collision.gameObject);
            //if (obstacle)
            //{
            //    ani.Attack();
            //    obstacle.GetDamage();
            //}
            Hero hero = Hero.Instance;
            if (target == hero.gameObject)
            {
                ani.Attack();
                hero.GetDamage(AttackValue, gameObject);
                //inContact = true;
            }
        }
        else if (entity.relation == Relation.AggressiveToPlayer)
        {
            Hero hero = Hero.Instance;
            if (target == hero.gameObject)
            {
                ani.Attack();
                hero.GetDamage(AttackValue, gameObject);
                //inContact = true;
            }
        }
        else if (entity.relation == Relation.FrendlyToPlayer)
        {
            //Decor dec = ListsOfObjects.GetDecorOfGameObject(collision.gameObject);
            //if (dec)
            //{
            //    ani.Attack();
            //    dec.GetDamage();
            //}
            Monster monster = ListsOfObjects.GetMonsterOfGameObject(target);
            if (monster)
            {
                ani.Attack();
                monster.GetDamage(AttackValue, gameObject);
            }
            //Obstacle obstacle = ListsOfObjects.GetObstacleOfGameObject(collision.gameObject);
            //if (obstacle)
            //{
            //    ani.Attack();
            //    obstacle.GetDamage();
            //}
        }
    }
}
