using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{

    public Hero hero;
    public float kSpeed = 1f;
    public int kProtection = 1;
    private int countOfBosters = 0;


    private void Awake()
    {
        if (hero == null)
        {
            hero = Hero.Instance;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Booster")
        {
            hero.IncreasedSpeed(kSpeed);
            hero.IncreasedProtection(kProtection);
            countOfBosters++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Booster")
        {
            hero.DecreasedSpeed(kSpeed);
            hero.DecreasedProtection(kProtection);
            countOfBosters--;
        }
    }

    private void Update()
    {
        transform.position = hero.transform.position;
    }


    public void RemovePositiveEffects()
    {
        for (int i = 0; i < countOfBosters; i++)
        {
            hero.DecreasedSpeed(kSpeed);
            hero.DecreasedProtection(kProtection);
        }
    }
}
