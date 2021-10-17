using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCommon : MonoBehaviour
{
    private Image pic;
    private Text txt;

    // Start is called before the first frame update
    void Start()
    {
        pic = GetComponentInChildren<Image>();
        txt = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
