using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    bool isShaking = false;
    float shake = .2f;
    float health = 5;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (health <= 0)
        {
            DestroyItObject();
            //Invoke("DestroyItObject", .5f);
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
            health--;
        }

    }

    private void StopShaking()
    {
        isShaking = false;
        transform.position = pos;
    }


    void DestroyItObject()
    {
        Destroy(gameObject);
    }
}
