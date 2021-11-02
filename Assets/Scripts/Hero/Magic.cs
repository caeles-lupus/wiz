using System.Collections;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private Coroutine coroutineWall;

    [Header("Связки")]
    public Hero hero;
    public GameObject Wall;

    [Header("Параметры магии \"Стена\"")]
    // Wall
    public float Distance = 4f;
    public float Period = 0.1f;
    public float TimeOfGrowing = 1f;
    public float PeriodOfLifeOfWall = 10f;
        
    private bool wallGrowing = false;
    private float sizeOfGrownPart;
    private float nextActionTime = 0.0f;
    BoxCollider2D colliderOfWall;
    float heightOfWall;
    float increasedHeight = 0f;
    //================


    // Start is called before the first frame update
    void Start()
    {
        if (hero == null)
        {
            hero = Hero.Instance;
        }
        if (Wall == null)
        {
            Debug.LogError("К скрипту магии у героя не привязан объект \"стены\"");
        }

        Wall.SetActive(false);

        // A WALL
        // Коллайдер стены.
        colliderOfWall = Wall.GetComponent<BoxCollider2D>();
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
            if (Time.time > nextActionTime)
            {
                nextActionTime += Period;

                // execute block of code here
                wallUp();
            }
        }
    }
    /// <summary>
    /// Применение способности(магии).
    /// </summary>
    public void ToCast(typeAbility ability)
    {
        switch (ability)
        {
            case typeAbility.Wall:
                float costWall = 1f;
                if (hero.Mana < costWall)
                {
                    notEnoughMana();
                    return;
                }
                castWall();
                hero.SpendMana(costWall); // TODO определить оптимальное количество.
                break;

            case typeAbility.Fireball:
                float costFireball = 1f;
                if (hero.Mana < costFireball)
                {
                    notEnoughMana();
                    return;
                }
                castFireball();
                hero.SpendMana(costFireball); // TODO определить оптимальное количество.
                break;

            case typeAbility.Fly:
                float costFly = 1f;
                if (hero.Mana < costFly)
                {
                    notEnoughMana();
                    return;
                }
                castFly();
                hero.SpendMana(costFly); // TODO определить оптимальное количество.
                break;

            case typeAbility.Heal:
                float costHeal = 1f;
                if (hero.Mana < costHeal)
                {
                    notEnoughMana();
                    return;
                }
                castHeal();
                hero.SpendMana(costHeal); // TODO определить оптимальное количество.
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void notEnoughMana()
    {
        // TODO: выдавать сообщение или мигать манабаром.
        return;
    }

    /// <summary>
    /// Определяет начальное расположение для стены.
    /// </summary>
    void toPlantWall()
    {
        float dirDistance = Distance * hero.Direction;
        float newY = transform.position.y - heightOfWall;
        Wall.transform.position = new Vector3(transform.position.x + dirDistance, newY);
        increasedHeight = 0f;
    }

    /// <summary>
    /// Каст "стены".
    /// </summary>
    void castWall()
    {
        toPlantWall();
        // Показываем стену.
        Wall.SetActive(true);
        // Это включает "рост" стены в методе Update().
        wallGrowing = true;
    }

    void wallUp()
    {
        Wall.transform.position = 
            new Vector3(Wall.transform.position.x, Wall.transform.position.y + sizeOfGrownPart);
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
        Wall.SetActive(false);
    }


    void castFireball()
    {

    }

    void castFly()
    {

    }

    void castHeal()
    {

    }
}
