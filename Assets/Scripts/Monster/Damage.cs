using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Entity
{
    private float speed = 3.5f;
    private Vector3 dir;
    private SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        dir = transform.right;
    }

    private void Move()
    {
        // Направление
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            transform.position + transform.up * .1f + transform.right * dir.x * .7f, .1f);
        
        // Движение
        if (colliders.Length > 0) dir *= -1f;
        transform.position = Vector3.MoveTowards(
            transform.position, transform.position + dir * speed, Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }

}
