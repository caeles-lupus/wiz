<<<<<<< Updated upstream
<<<<<<< Updated upstream
using UnityEngine;

[RequireComponent(typeof(Effect))]
public class Entity : MonoBehaviour
{

=======
=======
>>>>>>> Stashed changes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    [Header("���������")]
    /// <summary>
    /// �� ���������/�� �����������.
    /// </summary>
    public bool Immortal = false;
    /// <summary>
    /// ����� ���������/��������.
    /// </summary>
    public float Health = 5;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    
=======

>>>>>>> Stashed changes
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
    /// ��������� � ����������.
    /// </summary>
<<<<<<< Updated upstream
<<<<<<< Updated upstream
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
            GetDamage();
        }
    }

=======
    public Relation relation = Relation.AggressiveToPlayer;
>>>>>>> Stashed changes
=======
    public Relation relation = Relation.AggressiveToPlayer;
>>>>>>> Stashed changes

    /// <summary>
    /// ��������� ����� ���������.
    /// </summary>
    public virtual void GetDamage()
    {
        if (Immortal) return;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        effectOfDamage.EffectStart();
=======

>>>>>>> Stashed changes
=======

>>>>>>> Stashed changes
        Health--;
        if (Health <= 0)
        {
            Die();
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
            //Invoke("DestroyItObject", .5f);
>>>>>>> Stashed changes
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
