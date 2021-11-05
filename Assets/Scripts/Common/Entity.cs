using UnityEngine;

public enum TypeOfEntity
{
    Obstacle,
    Monster,
    Decor,
    Hero
}

public enum Relation
{
    FrendlyToAll,
    FrendlyToPlayer,
    AggressiveToAll,
    AggressiveToPlayer,
}

public class Entity : MonoBehaviour
{
    [Header("���������")]
    /// <summary>
    /// �� ���������/�� �����������.
    /// </summary>
    public bool Immortal = false;
    /// <summary>
    /// ������� ����� ���������/��������.
    /// </summary>
    public float Health = 10;
    /// <summary>
    /// ������������ ����� ��������.
    /// </summary>
    public float MaxHealth = 10;
    /// <summary>
    /// 
    /// </summary>
    //public float RegenerationHealth = 0f;

    [Header("����")]
    /// <summary>
    /// �� ���������� �����.
    /// </summary>
    public bool NoMana = false;
    /// <summary>
    /// ������� ����� ���������/��������.
    /// </summary>
    public float Mana = 10;
    /// <summary>
    /// ������������ ����� ��������.
    /// </summary>
    public float MaxMana = 10;
    /// <summary>
    /// 
    /// </summary>
    //public float RegenerationMana = 0f;

    /// <summary>
    /// ��������� � ����������.
    /// </summary>
    [Header("���������")]
    public Relation relation = Relation.AggressiveToPlayer;
    
    /// <summary>
    /// ������ ��� ��������� �����.
    /// </summary>
    [Header("������ ��������� �����")]
    public Effect effectOfDamage;

    protected TypeOfEntity myType;
    //======================================================
    //======================================================

    public void Update()
    {

    }

    public void Start()
    {
        if (effectOfDamage == null)
        {
            //effectOfDamage = new Effect();
            Debug.Log("� ������� " + gameObject.name + " �� �������� ������ ����������/�������!");
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Immortal) return;

        if (collision.gameObject.name == "Attack_Staff")
        {
            GetDamage(Hero.Instance.Attack);
        }
    }

    public virtual void Heal(float HealValue)
    {
        if (Immortal) return;
        //if (effectOfHeal) effectOfHeal.EffectStart();
        Health += HealValue;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }


    /// <summary>
    /// ��������� ����� ���������.
    /// </summary>
    public virtual void GetDamage(float DamageValue, GameObject attacker = null)
    {
        if (Immortal) return;
        if (effectOfDamage) effectOfDamage.EffectStart();
        Health -= DamageValue;
        if (Health <= 0)
        {
            Health = 0;
            Die(attacker);
        }
    }

    public virtual void Die(GameObject attacker = null)
    {
        ListsOfObjects.RemoveObj(this.gameObject);

        Destroy(this.gameObject);
    }

    public virtual void RestoringMana(float RestoringValue)
    {
        if (NoMana) return;
        //if (effectOfRestoringMana) effectOfRestoringMana.EffectStart();
        Mana += RestoringValue;
        if (Mana > MaxMana)
        {
            Mana = MaxMana;
        }
    }

    /// <summary>
    /// ����� �����.
    /// </summary>
    public virtual void SpendMana(float spentMana)
    {
        if (NoMana) return;
        //if (effectOfSpendMana) effectOfSpendMana.EffectStart();
        Mana -= spentMana;
        if (Mana <= 0)
        {
            Mana = 0;
            //???
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Alert(GameObject intruder, TypeOfEntity typeOfEntity)
    {
        Debug.Log("��������! � ������� ������� " + gameObject.name + " ������� ������ " + intruder.name);
    }
}
