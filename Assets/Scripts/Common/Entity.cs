using UnityEngine;

[RequireComponent(typeof(Effect))]
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
    //public float Regeneration = 0f;

    public enum Relation
    {
        FrendlyToAll,
        FrendlyToPlayer,
        AggressiveToAll,
        AggressiveToPlayer,
    }

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
    public virtual void GetDamage(float DamageValue)
    {
        if (Immortal) return;
        if (effectOfDamage) effectOfDamage.EffectStart();
        Health -= DamageValue;
        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    public virtual void Die()
    {
        ListsOfObjects.RemoveObj(this.gameObject);

        Destroy(this.gameObject);
    }

}
