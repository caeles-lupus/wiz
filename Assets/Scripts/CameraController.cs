using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform hero;

    private void Awake()
    {
        if (!hero)
        {
            hero = FindObjectOfType<Hero>().transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //pos = player.position;
        //pos.z = -10f;
        //pos.y = offsetY + pos.y; // offsetY - высота, на которую будет "задрана" камера.
        //transform.position = Vector3.Lerp(transform.position, pos, 2f * Time.deltaTime);

        transform.position = new Vector3(hero.position.x, 7.20f + hero.position.y, -10f);

    }
}

