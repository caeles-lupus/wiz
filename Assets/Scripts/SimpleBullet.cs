using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public GameObject BigMother;
    
    private bool isReady;

    // Start is called before the first frame update
    void Start()
    {
        Collider2D coll = GetComponent<Collider2D>();
        isReady = coll != null && BigMother != null;
        if (coll != null)
        {
            coll.isTrigger = true;
        }
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
            AI AIOfMother = BigMother.GetComponent<AI>();
            if (entity != null && AIOfMother) entity.GetDamage(AIOfMother.AttackValue);
        }
    }
}
