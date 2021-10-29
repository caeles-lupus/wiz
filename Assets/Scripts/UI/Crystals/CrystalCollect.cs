using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalCollect : MonoBehaviour
{
    public static int CrystalCount;
    private Text ñrystalCounter;

    // Start is called before the first frame update
    void Start()
    {
        ñrystalCounter = GetComponent<Text>();
        //ñrystalCounter.color = Color.white;
        CrystalCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ñrystalCounter.text = "x" + CrystalCount.ToString();
    }
}
