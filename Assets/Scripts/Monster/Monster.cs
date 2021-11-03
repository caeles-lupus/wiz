using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ListsOfObjects.AddMonster(this);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

    }

    public override void Die(GameObject attacker = null)
    {
        StatisticCollector.MonsterCount++;
        base.Die(attacker);
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }
}
