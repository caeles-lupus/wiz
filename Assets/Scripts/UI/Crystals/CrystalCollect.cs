using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalCollect : MonoBehaviour
{
    public static int CrystalCount;
    private Text �rystalCounter;

    // Start is called before the first frame update
    void Start()
    {
        �rystalCounter = GetComponent<Text>();
        //�rystalCounter.color = Color.white;
        CrystalCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        �rystalCounter.text = "x" + CrystalCount.ToString();
    }
}
