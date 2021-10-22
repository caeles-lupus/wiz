using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Entity
{
    //TODO: чтобы можно было задавать стартовое направление.
    public enum dir
    {
        toLeft,
        toRight
    };
    /// <summary>
    /// Стартовое направление.
    /// </summary>
    public dir StartDrirection = dir.toLeft;

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

    private bool chill;
    private bool angry;
    private bool goBack;
    bool MoveRight;

    void Awake()
    {
        startPos = transform.position;
        MoveRight = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //sprite = GetComponent<SpriteRenderer>();
        //chill = true;
        //angry = false;
        //goBack = false;
    }
    /// <summary>
    /// Обычное движение.
    /// </summary>
    void Chill()
    {
        if (transform.position.x > (startPos.x + PositionOfPatrol))
        {
            Turn(false);
            MoveRight = false;
        }
        else if (transform.position.x < (startPos.x - PositionOfPatrol))
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
        transform.position = Vector2.MoveTowards(transform.position, Hero.Instance.transform.position, Speed * Time.deltaTime);
    }
    /// <summary>
    /// Домой.
    /// </summary>
    void GoBack()
    {
        Turn(transform.position.x < startPos.x);
        transform.position = Vector2.MoveTowards(transform.position, startPos, Speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - startPos.x) < PositionOfPatrol && transform.position.y == startPos.y && !angry)
        {
            chill = true;
        }

        if (Vector2.Distance(transform.position, Hero.Instance.transform.position) < StoppingDistance)
        {
            angry = true;
            chill = false;
            goBack = false;
        }

        if (Vector2.Distance(transform.position, Hero.Instance.transform.position) > StoppingDistance)
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
        if (collision.gameObject != Hero.Instance.gameObject)
        {
            FlipX();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MovingRight"></param>
    private void Turn(bool MovingRight = false)
    {
        var x = Mathf.Abs(transform.localScale.x);
        if (!MovingRight)
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-x, transform.localScale.y, transform.localScale.z);
    }

    /// <summary>
    /// 
    /// </summary>
    private void FlipX()
    {
        var x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

}
