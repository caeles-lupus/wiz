using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : Effect
{

    [Header("Параметры эффекта")]
    public float PowerOfShake = .2f;
    Vector3 pos;

    public override void EffectStart()
    {
        base.EffectStart();
        pos = transform.position;
        Invoke("EffectStop", TimeOfWorking);
    }

    private new void EffectStop()
    {
        transform.position = pos;
        base.EffectStop();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isWorked)
        {
            Vector3 tmpv3 = new Vector3();
            Vector2 tmpv2 = new Vector2(pos.x, pos.y) + UnityEngine.Random.insideUnitCircle * PowerOfShake;
            tmpv3.x = tmpv2.x; tmpv3.y = tmpv2.y;
            tmpv3.z = pos.z;
            transform.position = tmpv3;
        }
    }
}
