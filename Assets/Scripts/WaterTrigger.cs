using System.Collections;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    public GameObject HeroBubbles;

    bool underWater = false;
    bool isChokes = false;
    float timeInWater = 0f;
    public float MaxTimeUnderWaterSec = 10f;
    private Coroutine coroutineO2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Если под водой, то
        if (underWater)
        {
            // Считаем время сколько уже под водой
            timeInWater += Time.deltaTime;
            // Если слишком долго, то отнимаем здоровье.
            if (timeInWater > MaxTimeUnderWaterSec && !isChokes)
            {
                isChokes = true;

                if (coroutineO2 != null) StopCoroutine(coroutineO2);
                coroutineO2 = StartCoroutine(chokes());
            }
        }
    }

    IEnumerator chokes()
    {
        while (isChokes)
        {
            yield return new WaitForSeconds(1f);
            Hero.Instance.GetDamage(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            if (HeroBubbles != null)
            {
                HeroBubbles.SetActive(true);
                underWater = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            if (HeroBubbles != null)
            {
                HeroBubbles.SetActive(false);
                underWater = false;
                timeInWater = 0f;
                isChokes = false;
            }
        }
    }
}
