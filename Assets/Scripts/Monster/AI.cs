using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���������, ��� ��� ������� ������� ��������� ������ ���� Entity, ���� ���������� �� ��.
[RequireComponent(typeof(Entity))]

public class AI: MonoBehaviour
{
    [Header("��������")]
    public Entity entity;

    //TODO: ����� ����� ���� �������� ��������� �����������.
    public enum dir
    {
        toLeft,
        toRight
    };

    [Header("����")]
    /// <summary>
    /// True - ���� ������ ��������.
    /// </summary>
    public bool isFlying = false;
    
    [Header("���������")]
    /// <summary>
    /// ����� ����������
    /// </summary>
    public bool isCompanion = false;
    /// <summary>
    /// Friend.
    /// </summary>
    public GameObject Friend;
    /// <summary>
    /// ���������� �� ������� �� "�����" �����, �� ������� ����� ���������.
    /// </summary>
    [Range(2, 10000)] public float DistanceOfFollow = 10f;

    [Header("�����")]
    /// <summary>
    /// ��������� �����������.
    /// </summary>
    public dir StartDrirection;
    /// <summary>
    /// �������� ������������.
    /// </summary>
    public float Speed = 3.5f;
    /// <summary>
    /// ���������� �� ������� �� �����������.
    /// </summary>
    [Range(2, 10000)] public int DistanceOfPatrol = 10;
    /// <summary>
    /// ����������, �� ������� ��������� ���.
    /// </summary>
    [Range(5, 10000)] public float DistanceOfArgession = 5;

    /// <summary>
    /// ��������� �����.
    /// </summary>
    private Vector3 startPos;

    private bool chill;
    private bool angry;
    private bool goBack;
    private bool follow;

    bool MoveRight;

    void Awake()
    {
        startPos = transform.position;
        MoveRight = StartDrirection == dir.toRight;
        //GameObject go = gameObject;
    }

    void Follow()
    {
        Turn(transform.position.x < Friend.transform.position.x);
        // ���� ��������.
        if (isFlying)
        {
            // ���������� ���������� ������ ��. TODO: �� ��������� ��� ������ ������� �����.
            float FriendY = Friend.GetComponent<CapsuleCollider2D>().bounds.max.y + 0.5f;
            // ������� ����� ������-����.
            Vector2 target = new Vector2(Friend.transform.position.x, FriendY);
            // ������������ �� ����� ������.
            transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        }
        else
        {
            Vector2 target = new Vector2(Friend.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// ������� ��������.
    /// </summary>
    void Chill()
    {
        if (transform.position.x > (startPos.x + DistanceOfPatrol))
        {
            Turn(false);
            MoveRight = false;
        }
        else if (transform.position.x < (startPos.x - DistanceOfPatrol))
        {
            Turn(true);
            MoveRight = true;
        }

        if (MoveRight)
        {
            transform.position = new Vector2(transform.position.x + Speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - Speed * Time.deltaTime, transform.position.y);
        }
    }
    /// <summary>
    /// ���.
    /// </summary>
    void Angry()
    {
        Turn(transform.position.x < Hero.Instance.transform.position.x);
        if (isFlying)
        {
            transform.position = Vector2.MoveTowards(transform.position, Hero.Instance.transform.position, Speed * Time.deltaTime);
        }
        else
        {
            Vector2 target = new Vector2(Hero.Instance.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        }
    }
    /// <summary>
    /// �����.
    /// </summary>
    void GoBack()
    {
        Turn(transform.position.x < startPos.x);
        if (isFlying)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos, Speed * Time.deltaTime);
        }
        else
        {
            Vector2 target = new Vector2(startPos.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (entity.relation != Entity.Relation.AggressiveToAll && isCompanion)
        {
            if (Friend)
            {
                if (Vector2.Distance(transform.position, Friend.transform.position) < DistanceOfFollow)
                {
                    follow = true;
                    angry = false;
                    chill = false;
                    goBack = false;
                }
            }
        }

        if (!angry && !follow && Mathf.Abs(transform.position.x - startPos.x) < DistanceOfPatrol && 
            transform.position.y == startPos.y)
        {
            chill = true;
            goBack = false;
        }
        
        if (entity.relation == Entity.Relation.AggressiveToAll || entity.relation == Entity.Relation.AggressiveToPlayer &&
            Vector2.Distance(transform.position, Hero.Instance.transform.position) < DistanceOfArgession)
        {
            angry = true;
            chill = false;
            goBack = false;
        }

        if (!follow && Vector2.Distance(transform.position, Hero.Instance.transform.position) > DistanceOfArgession)
        {
            goBack = true;
            angry = false;
        }
        if (follow)
        {
            Follow();
        }
        if (chill)
        {
            Chill();
        }
        else if (angry)
        {
            Angry();
        }
        else if (goBack)
        {
            GoBack();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != Hero.Instance.gameObject)
        {
            FlipX();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MovingRight"></param>
    private void Turn(bool MovingRight = false)
    {
        var x = Mathf.Abs(transform.localScale.x);

        if (MovingRight ^ StartDrirection == dir.toRight) //Xor
            transform.localScale = new Vector3(-x, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    /// <summary>
    /// 
    /// </summary>
    private void FlipX()
    {
        var x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

}
