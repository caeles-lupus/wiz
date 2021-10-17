using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image Bar;
    public float Fill;

    // Start is called before the first frame update
    void Start()
    {
        Fill = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Fill -= Time.deltaTime * 0.1f;
        Bar.fillAmount = Fill;
    }
}
