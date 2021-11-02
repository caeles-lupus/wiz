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
    float startingPositionY;
    private Coroutine coroutineWall;



    // Start is called before the first frame update
    void Start()
    {
        if (hero == null)
        {
            hero = Hero.Instance;
        }

        gameObject.SetActive(false);
        startingPositionY = transform.position.y;
        // A WALL
        // Коллайдер стены.
        colliderOfWall = GetComponent<BoxCollider2D>();
        // Высота стены.
        heightOfWall = colliderOfWall.size.y * transform.localScale.y;
        // На сколько вырастает стена каждые Period сек.
        sizeOfGrownPart = heightOfWall / (TimeOfGrowing / Period);

        // A FIREBALL
        //....
    }

    // Update is called once per frame
    void Update()
    {
        if (wallGrowing)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= Period)
            {
                wallUp();
            }
        }
    }

    /// <summary>
    /// Каст "стены".
    /// </summary>
    public void castWall()
    {
        toPlantWall();
        // Показываем стену.
        gameObject.SetActive(true);
        // Это включает "рост" стены в методе Update().
        timePassed = 0f;
        wallGrowing = true;
    }

    /// <summary>
    /// Определяет начальное расположение для стены.
    /// </summary>
    void toPlantWall()
    {
        float dirDistance = Distance * hero.Direction;
        float newY = startingPositionY - heightOfWall;
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

            // Скрываем стену спустя k секунд.
            if (coroutineWall != null) StopCoroutine(coroutineWall);
            coroutineWall = StartCoroutine(hideWall(PeriodOfLifeOfWall));
        }
    }

    /// <summary>
    /// Скрываем стену через waitInSec секунд.
    /// </summary>
    IEnumerator hideWall(float waitInSec = 5f)
    {
        yield return new WaitForSeconds(waitInSec);
        gameObject.SetActive(false);
    }
}
