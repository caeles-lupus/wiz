using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presettings : MonoBehaviour
{
    [Header("Îáúåêòû")]
    public GameObject StartScreenObj;
    public GameObject Level1;
    public GameObject Level2;
    public GameObject StatisticScreenObj;
    public GameObject MagicWall;


    private void Awake()
    {
        if (StartScreenObj != null)
        {
            if (!StartScreenObj.activeSelf)
            {
                StartScreenObj.SetActive(true);
            }
        }

        if (Level1 != null)
        {
            if (!Level1.activeSelf)
            {
                Level1.SetActive(true);
            }
        }

        if (Level2 != null)
        {
            if (Level2.activeSelf)
            {
                Level2.SetActive(false);
            }
        }

        if (StatisticScreenObj != null)
        {
            if (StatisticScreenObj.activeSelf)
            {
                StatisticScreenObj.SetActive(false);
            }
        }

        if (MagicWall != null)
        {
            if (MagicWall.activeSelf)
            {
                MagicWall.SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
