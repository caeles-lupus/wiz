using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presettings : MonoBehaviour
{
    [Header("Активация/дезактивация объектов")]
    public GameObject[] ObjectsToActivate;
    public GameObject[] ObjectsToDeactivate;

    [Header("Объекты для перемещения")]
    public GameObject[] Objects;
    public Vector2[] Coordinates;

    [Header("TestMode")]
    public bool Test = true;
    public TestMode testModeScript;

    private void Awake()
    {
        if (Test)
        {
            testModeScript.DoIt();
            return;
        }

        foreach (var item in ObjectsToActivate)
        {
            item.SetActive(true);
        }

        foreach (var item in ObjectsToDeactivate)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < Objects.Length; i++)
        {
            Objects[i].transform.position = Coordinates[i];
        }

    }
    //x:-235 y:1.81
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
