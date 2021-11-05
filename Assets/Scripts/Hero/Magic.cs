using System.Collections;
using UnityEngine;

public class Magic : MonoBehaviour
{
    //private Coroutine coroutineWall;

    [Header("Связки")]
    public Hero hero;
    public GameObject Wall;

    private MagicWall magicWall;

    // Start is called before the first frame update
    void Start()
    {
        if (hero == null)
        {
            hero = Hero.Instance;
        }
        if (Wall == null)
        {
            Debug.LogError("К скрипту магии у героя не привязан объект магии \"стена\"");
        }
        else
        {
            magicWall = Wall.GetComponent<MagicWall>();
            if (magicWall == null)
            {
                //TODO: не найден скрипт у объекта
            }
        }


        // A FIREBALL
        //....
    }

    // Update is called once per frame
    void Update()
    {

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
                magicWall.gameObject.SetActive(true);
                magicWall.castWall();
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
