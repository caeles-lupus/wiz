using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWall : MonoBehaviour
{
    [Header("Связки")]
    public Hero hero;

    [Header("Параметры магии \"Стена\"")]
    // Wall
    //public float Distance = 4f;
    private float Distance = 0f;

    public float Period = 0.1f;
    public float TimeOfGrowing = 1f;
    public float PeriodOfLifeOfWall = 10f;

    private bool wallGrowing = false;
    private float sizeOfGrownPart;
    private float timePassed;
    BoxCollider2D colliderOfWall;
    float heightOfWall;
    float increasedHeight = 0f;
    private Coroutine coroutineHideWall;
    private Coroutine coroutineGrowWall;



    // Start is called before the first frame update
    void Start()
    {
        if (hero == null)
        {
            hero = Hero.Instance;
        }
        
        // A WALL
        // Коллайдер стены.
        colliderOfWall = GetComponent<BoxCollider2D>();
        // Высота стены.
        heightOfWall = colliderOfWall.size.y * transform.localScale.y;
        // На сколько вырастает стена каждые Period сек.
        sizeOfGrownPart = heightOfWall / (TimeOfGrowing / Period);
    }

    // Update is called once per frame
    void Update()
    {
        //if (wallGrowing)
        //{
        //    timePassed += Time.deltaTime;
        //    if (timePassed >= Period)
        //    {
        //        wallUp();
        //    }
        //}
    }

    /// <summary>
    /// Каст "стены".
    /// </summary>
    public void castWall()
    {
        toPlantWall();
        // Показываем стену.
        Show();
        // Это включает "рост" стены в методе Update().
        //timePassed = 0f;
        wallGrowing = true;


        if (coroutineGrowWall != null) StopCoroutine(coroutineGrowWall);
        coroutineGrowWall = StartCoroutine(growWall(Period));
    }

    /// <summary>
    /// Определяет начальное расположение для стены.
    /// </summary>
    void toPlantWall()
    {
        float dirDistance = Distance * hero.Direction;
        float newY = hero.transform.position.y - heightOfWall;
        transform.position = new Vector3(hero.transform.position.x + dirDistance, newY);
        increasedHeight = 0f;
    }


    void wallUp()
    {
        transform.position =
            new Vector3(transform.position.x, transform.position.y + sizeOfGrownPart);

        increasedHeight += sizeOfGrownPart;
        if (increasedHeight >= heightOfWall)
        {
            // Перестаем расти.
            wallGrowing = false;

            // Скрываем стену спустя PeriodOfLifeOfWall секунд.
            if (coroutineHideWall != null) StopCoroutine(coroutineHideWall);
            coroutineHideWall = StartCoroutine(hideWall(PeriodOfLifeOfWall));
        }
    }

    IEnumerator growWall(float waitInSec = 5f)
    {
        while (wallGrowing)
        {
            yield return new WaitForSeconds(waitInSec);
            wallUp();
        }
    }

    /// <summary>
    /// Скрываем стену через waitInSec секунд.
    /// </summary>
    IEnumerator hideWall(float waitInSec = 5f)
    {
        yield return new WaitForSeconds(waitInSec);
        Hide();
    }

    void Hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    void Show()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }
}
