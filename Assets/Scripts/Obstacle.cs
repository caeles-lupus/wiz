using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Entity
{
    //private bool inContact = false;


    private new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (relation == Relation.AggressiveToAll)
        {
            //Decor dec = ListsOfObjects.GetDecorOfGameObject(collision.gameObject);
            //if (dec)
            //{
            //    dec.GetDamage();
            //}
            Monster monster = ListsOfObjects.GetMonsterOfGameObject(collision.gameObject);
            if (monster)
            {
                monster.GetDamage();
            }
            //Obstacle obstacle = ListsOfObjects.GetObstacleOfGameObject(collision.gameObject);
            //if (obstacle)
            //{
            //    obstacle.GetDamage();
            //}
            Hero hero = Hero.Instance;
            if (collision.gameObject == hero.gameObject)
            {
                hero.GetDamage();
                //inContact = true;
            }
        }
        else if (relation == Relation.AggressiveToPlayer)
        {
            Hero hero = Hero.Instance;
            if (collision.gameObject == hero.gameObject)
            {
                hero.GetDamage();
                //inContact = true;
            }
        }
        else if (relation == Relation.FrendlyToPlayer)
        {
            //Decor dec = ListsOfObjects.GetDecorOfGameObject(collision.gameObject);
            //if (dec)
            //{
            //    dec.GetDamage();
            //}
            Monster monster = ListsOfObjects.GetMonsterOfGameObject(collision.gameObject);
            if (monster)
            {
                monster.GetDamage();
            }
            //Obstacle obstacle = ListsOfObjects.GetObstacleOfGameObject(collision.gameObject);
            //if (obstacle)
            //{
            //    obstacle.GetDamage();
            //}
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject == Hero.Instance.gameObject)
        //{
        //    inContact = false;
        //}
    }

    new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ListsOfObjects.AddObstacle(this);
    }

    private void FixedUpdate()
    {

        //if (inContact)
        //    HeroDamage();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    //void HeroDamage()
    //{
    //    Hero.Instance.GetDamage();
    //}
}
