using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Entity
{
    //private bool inContact = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO: GetGameObjectOfDecor переделать на GetDecorOfGameObject. ƒолжен возвращать мой тип.
        if (collision.gameObject == ListsOfObjects.GetGameObjectOfDecor())
        {

        }

        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            //inContact = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject == Hero.Instance.gameObject)
        //{
        //    inContact = false;
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {

        //if (inContact)
        //    HeroDamage();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //void HeroDamage()
    //{
    //    Hero.Instance.GetDamage();
    //}
}
