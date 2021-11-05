using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{

    public Hero hero;

    private void Awake()
    {
        if (hero == null)
        {
            hero = Hero.Instance;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO: а вдруг не Bird??? Нужно больше абстракции!!!
        if (collision.gameObject.name == "Bird")
        {
            hero.IncreasedSpeed(0.5f);
            hero.IncreasedProtection(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //TODO: а вдруг не Bird??? Нужно больше абстракции!!!
        if (collision.gameObject.name == "Bird")
        {
            hero.DecreasedSpeed(0.5f);
            hero.DecreasedProtection(1);
        }
    }

    private void Update()
    {
        transform.position = hero.transform.position;
    }
}
