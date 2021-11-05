using System.Collections;
using UnityEngine;

public class Magic : MonoBehaviour
{
    //private Coroutine coroutineWall;

    [Header("������")]
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
            Debug.LogError("� ������� ����� � ����� �� �������� ������ ����� \"�����\"");
        }
        else
        {
            magicWall = Wall.GetComponent<MagicWall>();
            if (magicWall == null)
            {
                //TODO: �� ������ ������ � �������
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
    /// ���������� �����������(�����).
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
                hero.SpendMana(costWall); // TODO ���������� ����������� ����������.
                break;

            case typeAbility.Fireball:
                float costFireball = 1f;
                if (hero.Mana < costFireball)
                {
                    notEnoughMana();
                    return;
                }
                castFireball();
                hero.SpendMana(costFireball); // TODO ���������� ����������� ����������.
                break;

            case typeAbility.Fly:
                float costFly = 1f;
                if (hero.Mana < costFly)
                {
                    notEnoughMana();
                    return;
                }
                castFly();
                hero.SpendMana(costFly); // TODO ���������� ����������� ����������.
                break;

            case typeAbility.Heal:
                float costHeal = 1f;
                if (hero.Mana < costHeal)
                {
                    notEnoughMana();
                    return;
                }
                castHeal();
                hero.SpendMana(costHeal); // TODO ���������� ����������� ����������.
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
        // TODO: �������� ��������� ��� ������ ���������.
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
