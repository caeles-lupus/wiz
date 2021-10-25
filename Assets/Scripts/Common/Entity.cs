using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("���������")]
    /// <summary>
    /// �� ���������/�� �����������.
    /// </summary>
    public bool Immortal = false;
    /// <summary>
    /// ����� ���������/��������.
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
    /// ��������� � ����������.
    /// </summary>
    public Relation relation = Relation.AggressiveToPlayer;

    /// <summary>
    /// ��������� ����� ���������.
    /// </summary>
    public virtual void GetDamage()
    {
        if (Immortal) return;

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
