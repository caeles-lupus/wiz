using UnityEngine;
using UnityEngine.UI;

public class ParentBar : MonoBehaviour
{
    public Image Bar;
    public Text text;

    private float value;
    private float maxValue;

    // Start is called before the first frame update
    public virtual void Start()
    {
        value = 1f;
        maxValue = 1f;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Bar.fillAmount = getConvertVal();
        text.text = value.ToString("0.0") + "/"+ maxValue.ToString("0.0");
    }


    public virtual float getConvertVal()
    {
        float resVal = value / maxValue;

        if (resVal > 1f) resVal = 1f;
        else if (resVal < 0f) resVal = 0f;
        return resVal;
    }

    public virtual void UpdateValue(float val, float maxVal)
    {
        this.value = val;
        this.maxValue = maxVal;
    }
}
