<<<<<<< Updated upstream
using UnityEngine;

[RequireComponent(typeof(Effect))]
public class Entity : MonoBehaviour
{

=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
>>>>>>> Stashed changes
    [Header("Прочность")]
    /// <summary>
    /// Не убиваемое/не разрушаемое.
    /// </summary>
    public bool Immortal = false;
    /// <summary>
    /// Запас прочности/здоровья.
    /// </summary>
    public float Health = 5;
<<<<<<< Updated upstream
    
=======

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
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
            GetDamage();
        }
    }

=======
    public Relation relation = Relation.AggressiveToPlayer;
>>>>>>> Stashed changes

    /// <summary>
    /// Получение урона существом.
    /// </summary>
    public virtual void GetDamage()
    {
        if (Immortal) return;
<<<<<<< Updated upstream
        effectOfDamage.EffectStart();
=======

>>>>>>> Stashed changes
        Health--;
        if (Health <= 0)
        {
            Die();
<<<<<<< Updated upstream
=======
            //Invoke("DestroyItObject", .5f);
>>>>>>> Stashed changes
        }
    }

    public virtual void Die()
    {
        ListsOfObjects.RemoveObj(this.gameObject);
        Destroy(this.gameObject);
    }

}
