using UnityEngine;

[RequireComponent(typeof(Effect))]
public class Entity : MonoBehaviour
{
    [Header("Прочность")]
    /// <summary>
    /// Не убиваемое/не разрушаемое.
    /// </summary>
    public bool Immortal = false;
    /// <summary>
    /// Текущий запас прочности/здоровья.
    /// </summary>
    public float Health = 10;
    /// <summary>
    /// Максимальный запас здоровья.
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
    /// Отношение к окружающим.
    /// </summary>
    [Header("Отношения")]
    public Relation relation = Relation.AggressiveToPlayer;
    
    /// <summary>
    /// Эффект при получении урона.
    /// </summary>
    [Header("Эффект получения урона")]
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
            Debug.Log("К объекту " + gameObject.name + " не привязан эффект разрушения/ранения!");
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
    /// Получение урона существом.
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
