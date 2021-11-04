using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_ : MonoBehaviour {


    public static CameraController_ Instance;

    public GameObject Target;
    public int Smoothvalue =2;
    public float PosY = 1;


    // Use this for initialization
    public Coroutine my_co;

    void Start()
    {
        Instance = this;
    }


    void Update()
    {
        Vector3 Targetpos = new Vector3(Target.transform.position.x, Target.transform.position.y + PosY, -100);
        transform.position = Vector3.Lerp(transform.position, Targetpos, Time.deltaTime * Smoothvalue);



    }



}
