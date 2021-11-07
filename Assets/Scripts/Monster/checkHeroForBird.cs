using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkHeroForBird : MonoBehaviour
{
    public Hero hero;
    public AI bird;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == hero.gameObject)
        {
            bird.YouCanMove();
            gameObject.SetActive(false);
        }
    }
}
