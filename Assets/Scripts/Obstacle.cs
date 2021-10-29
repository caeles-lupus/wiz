using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class TargetAndItsTiming: IEquatable<TargetAndItsTiming>
{
    public GameObject Target;
    public float Timeattack;

    public TargetAndItsTiming(GameObject target, float timeattack)
    {
        this.Target = target;
        this.Timeattack = timeattack;
    }

    public override bool Equals(object obj) => this.Equals(obj as TargetAndItsTiming);

    public static bool operator ==(TargetAndItsTiming var1, TargetAndItsTiming var2)
    {
        if (var1 is null)
        {
            if (var2 is null)
            {
                return true;
            }

            // Only the left side is null.
            return false;
        }
        // Equals handles case of null on right side.
        return var1.Equals(var2);
    }

    public static bool operator !=(TargetAndItsTiming var1, TargetAndItsTiming var2)
    {
        return !(var1 == var2);
    }

    public bool Equals(TargetAndItsTiming p)
    {
        if (p is null)
        {
            return false;
        }

        // Optimization for a common success case.
        if (System.Object.ReferenceEquals(this, p))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (this.GetType() != p.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return (Target == p.Target);// && (Y == p.Y);
    }
    public override int GetHashCode() => (Target, Timeattack).GetHashCode();

}

public class Obstacle : Entity
{
    //private bool inContact = false;

    /// <summary>
    /// —ила атаки. Ёта величина будет отниматьс€ каждый кадр пока "враг" соприкасаетс€ с "нами".
    /// </summary>
    [Header("ѕараметры бо€")]
    [Range (0.1f, 1000f)]
    public float AttackValue = 1f;
    [Range (0.1f, 1000f)]
    public float AttackPeriod = 0.5f;

    private List<TargetAndItsTiming> Targets;

    private void OnCollisionStay2D(Collision2D collision)
    {
        TargetAndItsTiming target = Targets.Find(trg => trg.Target == collision.gameObject);
        if (target != null)
        {
            target.Timeattack += Time.deltaTime;
            if (target.Timeattack >= AttackPeriod)
            {
                target.Timeattack = 0;
                attackTarget(target.Target.gameObject);
            }
        }
        //base.OnCollisionStay2D(collision);
        
    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        TargetAndItsTiming target = new TargetAndItsTiming(collision.gameObject, 0);
        if (!Targets.Contains(target))
        {
            Targets.Add(target);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        TargetAndItsTiming target = new TargetAndItsTiming(collision.gameObject, 0);
        if (Targets.Contains(target))
        {
            Targets.Remove(target);
        }
    }

    new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Targets = new List<TargetAndItsTiming>();
        ListsOfObjects.AddObstacle(this);
    }

    private void FixedUpdate()
    {

        //if (inContact)
        //    HeroDamage();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    void attackTarget(GameObject target)
    {
        if (relation == Relation.AggressiveToAll)
        {
            //Decor dec = ListsOfObjects.GetDecorOfGameObject(collision.gameObject);
            //if (dec)
            //{
            //    dec.GetDamage();
            //}
            Monster monster = ListsOfObjects.GetMonsterOfGameObject(target);
            if (monster)
            {
                monster.GetDamage(AttackValue);
            }
            //Obstacle obstacle = ListsOfObjects.GetObstacleOfGameObject(collision.gameObject);
            //if (obstacle)
            //{
            //    obstacle.GetDamage();
            //}
            Hero hero = Hero.Instance;
            if (target == hero.gameObject)
            {
                hero.GetDamage(AttackValue);
                //inContact = true;
            }
        }
        else if (relation == Relation.AggressiveToPlayer)
        {
            Hero hero = Hero.Instance;
            if (target == hero.gameObject)
            {
                hero.GetDamage(AttackValue);
                //inContact = true;
            }
        }
        else if (relation == Relation.FrendlyToPlayer)
        {
            //Decor dec = ListsOfObjects.GetDecorOfGameObject(collision.gameObject);
            //if (dec)
            //{
            //    dec.GetDamage();
            //}
            Monster monster = ListsOfObjects.GetMonsterOfGameObject(target);
            if (monster)
            {
                monster.GetDamage(AttackValue);
            }
            //Obstacle obstacle = ListsOfObjects.GetObstacleOfGameObject(collision.gameObject);
            //if (obstacle)
            //{
            //    obstacle.GetDamage();
            //}
        }
    }
}
