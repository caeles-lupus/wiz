//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;
    private float offsetY;

    private void Awake()
    {
        if (!player)
        {
            player = FindObjectOfType<Hero>().transform;
            offsetY = transform.position.y;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = player.position;
        pos.z = -10f;
        pos.y = offsetY + pos.y; // offsetY - высота, на которую будет "задрана" камера.
        transform.position = Vector3.Lerp(transform.position, pos, 2f * Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, pos, 1f/256f);
    }


    float Max(float val1, float val2)
    {
        return val1 > val2? val1 : val2;
    }
}

