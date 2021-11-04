using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Target;
    public TypeOfEntity TargetType;
    public bool AimBot;
    public float MaxDistance = 20f;
    public float Speed = 1f;
    public float AttackValue = 0f;

    float distance;
    Vector2 direction;

    bool isReady = true;
    bool lostAim = false;

    Entity entityOfTarget;

    // Start is called before the first frame update
    void Start()
    {
        if (Target == null)
        {
            Debug.LogError("У объекта " + gameObject.name + " в скрипте \"Bullet\" не задана цель (Target).");
            isReady = false;
        }
        if (isReady)
        {
            direction = Target.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReady) return;

        distance = Vector2.Distance(transform.position, Target.transform.position);
        if (!AimBot && !lostAim && distance <= MaxDistance)
        {
            direction = Target.transform.position;
        }
        else
        {
            lostAim = true;
        }

        MoveTo(direction);
    }

    void MoveTo(Vector2 place)
    {
        transform.position = Vector2.Lerp(transform.position, place, Speed * Time.deltaTime);
    }

    void ff()
    {
        entityOfTarget = Target.GetComponent<Entity>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Target)
        {
            entityOfTarget.GetDamage(AttackValue);
            //switch (TargetType)
            //{
            //    case TypeOfEntity.Obstacle:
            //        break;
            //    case TypeOfEntity.Monster:
            //        break;
            //    case TypeOfEntity.Decor:
            //        break;
            //    case TypeOfEntity.Hero:
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
