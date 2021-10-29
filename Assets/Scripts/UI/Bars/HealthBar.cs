using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Bar;
    public Text text;

    private float value;

    // Start is called before the first frame update
    void Start()
    {
        value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Bar.fillAmount = value;
        text.text = (value * 100).ToString() + "/100";
    }

    ///// <summary>
    ///// ”меньшает значение здоровь€ на заданное число.
    ///// </summary>
    ///// <param name="val">From 0 to 1</param>
    //public void Decrease(float val)
    //{
    //    val = Mathf.Max(0f, val);
    //    val = Mathf.Min(1f, val);
    //    value -= val;

    //    if (value > 1f) value = 1f;
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="val"></param>
    //public void Increase(float val)
    //{
    //    val = Mathf.Max(0f, val);
    //    val = Mathf.Min(1f, val);
    //    value += val;

    //    if (value < 0f) value = 0f;
    //}

    public void UpdateValue(float val, float maxVal)
    {
        val /= maxVal;
        value = val;
        if (value > 1f) value = 1f;
        else if (value < 0f) value = 0f;
    }
}
