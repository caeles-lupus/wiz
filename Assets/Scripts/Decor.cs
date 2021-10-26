using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decor : Entity
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ListsOfObjects.AddDecor(this);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    //=====================================================
    //=====================================================

    new private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
