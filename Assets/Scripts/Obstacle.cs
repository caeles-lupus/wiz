using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class TargetAndItsTiming : IEquatable<TargetAndItsTiming>
{
    public GameObject Target;
    public float TimeAttack;

    public TargetAndItsTiming(GameObject target, float timeattack)
    {
        this.Target = target;
        this.TimeAttack = timeattack;
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
    public override int GetHashCode() => (Target, TimeAttack).GetHashCode();

}

public class Obstacle : Entity
{
    /// <summary>
    /// Сила атаки. Эта величина будет отниматься каждый кадр пока "враг" соприкасается с "нами".
    /// </summary>
    [Header("Параметры боя")]
    [Range (0.1f, 1000f)]
    public float AttackValue = 1f;
    [Range (0.1f, 1000f)]
    public float AttackPeriod = 0.5f;

    private List<TargetAndItsTiming> Targets;

    private void OnCollisionStay2D(Collision2D collision)
    {
        //attackTarget(collision.gameObject);
        if (Targets.Count > 0)
        {
            TargetAndItsTiming target = Targets.Find(trg => trg.Target == collision.gameObject);
            if (target != null)
            {
                target.TimeAttack += Time.deltaTime;
                if (target.TimeAttack >= AttackPeriod)
                {
                    target.TimeAttack = 0;
                    attackTarget(target.Target.gameObject);
                }
            }
        }

        //base.OnCollisionStay2D(collision);

    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        // Выходим, если этот объект не подходит для атаки (земля, монетки, кристаллы).
        if (TagsSets.tagsNonTarget.Contains(collision.gameObject.tag)) return;
        //
        if (Targets.Count > 0)
        {
            TargetAndItsTiming foundTarget = Targets.Find(trg => trg.Target == collision.gameObject);
            if (foundTarget != null)
            {
                return;
            }
        }

        TargetAndItsTiming target = new TargetAndItsTiming(collision.gameObject, 0f);
        Targets.Add(target);

        attackTarget(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (Targets.Count > 0)
        {
            TargetAndItsTiming target = Targets.Find(trg => trg.Target == collision.gameObject);
            if (target != null)
            {
                Targets.Remove(target);
            }
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

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    void attackTarget(GameObject target)
    {
        // Выходим, если этот объект не подходит для атаки (земля, монетки, кристаллы).
        if (TagsSets.tagsNonTarget.Contains(target.tag)) return;

        //
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
