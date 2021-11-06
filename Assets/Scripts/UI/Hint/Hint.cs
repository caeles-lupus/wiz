using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    public bool FollowToMaster = false;
    public GameObject Master;

    public bool isTimed = false;
    public float MaxTime = 15f;
    float timeElapsed = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FollowToMaster)
        {
            if (Master != null)
            {
                var h = Master.GetComponent<CapsuleCollider2D>().size.y;
                transform.position = new Vector3(Master.transform.position.x, Master.transform.position.y + h/*3.4f*/, 0);
            }
        }

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
        if (Master != null && collision.gameObject == Master.gameObject) return;
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
