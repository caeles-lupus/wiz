using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollect : MonoBehaviour
{
    public static int CoinCount;
    private Text coinCounter;

    // Start is called before the first frame update
    void Start()
    {

        coinCounter = GetComponent<Text>();
        coinCounter.color = Color.white;
        CoinCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coinCounter.text = "x" + CoinCount.ToString();
    }
}
