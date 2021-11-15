using UnityEngine;

public class Radar : MonoBehaviour
{
    [Header("��������� ������")]
    [Range(5f, 500f)] public float Radius = 10f;
    [Header("����")]
    public Entity entity;

    Collider2D area;

    private void Awake()
    {

        area = GetComponent<Collider2D>();
        if (area == null)
        {
            Debug.LogError("�� ������ �������� ��������� � �������-������ \"" + gameObject.name + "\"");
        }
        else
        {
            if (area is CircleCollider2D)
            {
                (area as CircleCollider2D).radius = Radius;
            }
            area.isTrigger = true;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // �������, ���� ���� ������ �� �������� ��� ����� (�����, �������, ���������).
    //    if (TagsSets.tagsNonTarget.Contains(collision.tag)) return;

    //    //
    //    if (entity.relation == Relation.AggressiveToAll)
    //    {
    //        Decor dec = ListsOfObjects.GetDecorOfGameObject(collision.gameObject);
    //        if (dec)
    //        {
    //            entity.Alert(collision.gameObject, TypeOfEntity.Decor);
    //        }
    //        Monster monster = ListsOfObjects.GetMonsterOfGameObject(collision.gameObject);
    //        if (monster)
    //        {
    //            entity.Alert(collision.gameObject, TypeOfEntity.Monster);
    //        }
    //        Obstacle obstacle = ListsOfObjects.GetObstacleOfGameObject(collision.gameObject);
    //        if (obstacle)
    //        {
    //            entity.Alert(collision.gameObject, TypeOfEntity.Obstacle);
    //        }
    //        Hero hero = Hero.Instance;
    //        if (collision.gameObject == hero.gameObject)
    //        {
    //            entity.Alert(collision.gameObject, TypeOfEntity.Hero);
    //        }
    //    }
    //    else if (entity.relation == Relation.AggressiveToPlayer)
    //    {
    //        Hero hero = Hero.Instance;
    //        if (collision.gameObject == hero.gameObject)
    //        {
    //            entity.Alert(collision.gameObject, TypeOfEntity.Hero);
    //        }
    //    }
    //    else if (entity.relation == Relation.FrendlyToPlayer)
    //    {
    //        Decor dec = ListsOfObjects.GetDecorOfGameObject(collision.gameObject);
    //        if (dec)
    //        {
    //            entity.Alert(collision.gameObject, TypeOfEntity.Decor);
    //        }
    //        Monster monster = ListsOfObjects.GetMonsterOfGameObject(collision.gameObject);
    //        if (monster)
    //        {
    //            entity.Alert(collision.gameObject, TypeOfEntity.Monster);
    //        }
    //        Obstacle obstacle = ListsOfObjects.GetObstacleOfGameObject(collision.gameObject);
    //        if (obstacle)
    //        {
    //            entity.Alert(collision.gameObject, TypeOfEntity.Obstacle);
    //        }
    //    }

    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
