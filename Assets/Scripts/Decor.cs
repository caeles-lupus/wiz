using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decor : Entity
{

    bool isShaking = false;
    float shake = .2f;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        ListsOfObjects.AddDecor(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            Vector3 tmpv3 = new Vector3();
            Vector2 tmpv2 = new Vector2(pos.x, pos.y) + UnityEngine.Random.insideUnitCircle * shake;
            tmpv3.x = tmpv2.x; tmpv3.y = tmpv2.y;
            tmpv3.z = pos.z;
            transform.position = tmpv3;
        }
    }

    //=====================================================
    //=====================================================

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Attack_Staff")
        {
            isShaking = true;
            pos = transform.position;
            Invoke("StopShaking", .3f);
            GetDamage();
        }

    }

    private void StopShaking()
    {
        isShaking = false;
        transform.position = pos;
    }

}
