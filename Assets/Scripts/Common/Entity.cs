using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    /// <summary>
    /// Запас прочности/здоровья.
    /// </summary>
    public float Health = 5;

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
    public Relation relation = Relation.AggressiveToPlayer;

    /// <summary>
    /// Получение урона существом.
    /// </summary>
    public virtual void GetDamage()
    {
        Health--;
        if (Health <= 0)
        {
            Die();
            //Invoke("DestroyItObject", .5f);
        }
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }

}
