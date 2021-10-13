using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Entity
{
    /// <summary>
    /// 
    /// </summary>
    public float Speed = 3.5f;
    /// <summary>
    /// Расстояние на котором он патрулирует.
    /// </summary>
    public int PositionOfPatrol;
    /// <summary>
    /// Начальная точка.
    /// </summary>
    private Vector3 startPos;
    /// <summary>
    /// Расстояние, на котором кончается агр.
    /// </summary>
    public float StoppingDistance;

    private bool MovingRight;
    private bool chill;
    private bool angry;
    private bool goBack;


    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        MovingRight = false;
        sprite = GetComponent<SpriteRenderer>();
        startPos = transform.position;

        //chill = true;
        //angry = false;
        //goBack = false;
    }
    /// <summary>
    /// Обычное движение.
    /// </summary>
    void Chill()
    {
        if (transform.position.x > startPos.x + PositionOfPatrol)
        {
            MovingRight = false;
            sprite.flipX = false;
        }
        else if (transform.position.x < startPos.x - PositionOfPatrol)
        {
            MovingRight = true;
            sprite.flipX = true;
        }

        if (MovingRight)
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
        sprite.flipX = transform.position.x < Hero.Instance.transform.position.x;
        transform.position = Vector2.MoveTowards(transform.position, Hero.Instance.transform.position, Speed * Time.deltaTime);
    }
    /// <summary>
    /// Домой.
    /// </summary>
    void GoBack()
    {
        sprite.flipX = transform.position.x < startPos.x;
        transform.position = Vector2.MoveTowards(transform.position, startPos, Speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - startPos.x) < PositionOfPatrol && !angry)
        {
            chill = true;
        }

        if(Vector2.Distance(transform.position, Hero.Instance.transform.position) < StoppingDistance)
        {
            angry = true;
            chill = false;
            goBack = false;
        }

        if(Vector2.Distance(transform.position, Hero.Instance.transform.position) > StoppingDistance)
        {
            goBack = true;
            angry = false;
        }
        
        if (chill)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject == Hero.Instance.gameObject)
        //{
        //    Hero.Instance.GetDamage();
        //}
        if (collision.gameObject != Hero.Instance.gameObject)
        {
            MovingRight = !MovingRight;
            sprite.flipX = !sprite.flipX;
        }
    }

}
