using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{

    public bool isTimed = false;
    public float MaxTime = 15f;
    float timeElapsed = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimed) return;

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= MaxTime)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTimed) return;

        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ShowHint()
    {
        //ThisText.text = Sentence;
        //gameObject.SetActive(true);
    }

}
