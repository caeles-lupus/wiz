using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public float AttackValue;

    private bool isReady;

    // Start is called before the first frame update
    void Start()
    {
        Collider2D coll = GetComponent<Collider2D>();
        isReady = coll != null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReady)
        {
            Entity entity = collision.GetComponent<Entity>();
            if (entity != null) entity.GetDamage(AttackValue);
        }
    }
}
