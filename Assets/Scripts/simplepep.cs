using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplepep : Entity
{
    /// <summary>
    /// 
    /// </summary>
    public float Speed = 3.5f;
    /// <summary>
    
    /// <summary>
    /// Начальная точка.
    /// </summary>
    private Vector3 startPos;
    /// <summary>
    /// Расстояние, на котором кончается агр.
    /// </summary>
   

    private bool MovingRight;
    

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        MovingRight = false;
        //sprite = GetComponent<SpriteRenderer>();
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
        if (transform.position.x > startPos.x +10)
        {
            MovingRight = false;
           // sprite.flipX = false;
        }
        else if (transform.position.x < startPos.x - 10)
        {
            MovingRight = true;
           // sprite.flipX = true;
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


    // Update is called once per frame
    void Update()
    {
        Chill();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject == Hero.Instance.gameObject)
        //{
        //    Hero.Instance.GetDamage();
        //}
        //if (collision.gameObject != Hero.Instance.gameObject)
        //{
        //    MovingRight = !MovingRight;
        //    //sprite.flipX = !sprite.flipX;
        //}
    }

}
