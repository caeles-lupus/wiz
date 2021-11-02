using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWall : MonoBehaviour
{
    [Header("������")]
    public Hero hero;

    [Header("��������� ����� \"�����\"")]
    // Wall
    //public float Distance = 4f;
    private float Distance = 0f;

    public float Period = 0.1f;
    public float TimeOfGrowing = 1f;
    public float PeriodOfLifeOfWall = 10f;

    private bool wallGrowing = false;
    private float sizeOfGrownPart;
    private float timePassed;
    BoxCollider2D colliderOfWall;
    float heightOfWall;
    float increasedHeight = 0f;
    float startingPositionY;
    private Coroutine coroutineWall;



    // Start is called before the first frame update
    void Start()
    {
        if (hero == null)
        {
            hero = Hero.Instance;
        }

        gameObject.SetActive(false);
        startingPositionY = transform.position.y;
        // A WALL
        // ��������� �����.
        colliderOfWall = GetComponent<BoxCollider2D>();
        // ������ �����.
        heightOfWall = colliderOfWall.size.y * transform.localScale.y;
        // �� ������� ��������� ����� ������ Period ���.
        sizeOfGrownPart = heightOfWall / (TimeOfGrowing / Period);

        // A FIREBALL
        //....
    }

    // Update is called once per frame
    void Update()
    {
        if (wallGrowing)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= Period)
            {
                wallUp();
            }
        }
    }

    /// <summary>
    /// ���� "�����".
    /// </summary>
    public void castWall()
    {
        toPlantWall();
        // ���������� �����.
        gameObject.SetActive(true);
        // ��� �������� "����" ����� � ������ Update().
        timePassed = 0f;
        wallGrowing = true;
    }

    /// <summary>
    /// ���������� ��������� ������������ ��� �����.
    /// </summary>
    void toPlantWall()
    {
        float dirDistance = Distance * hero.Direction;
        float newY = startingPositionY - heightOfWall;
        transform.position = new Vector3(hero.transform.position.x + dirDistance, newY);
        increasedHeight = 0f;
    }


    void wallUp()
    {
        transform.position =
            new Vector3(transform.position.x, transform.position.y + sizeOfGrownPart);
        increasedHeight += sizeOfGrownPart;
        if (increasedHeight >= heightOfWall)
        {
            // ��������� �����.
            wallGrowing = false;

            // �������� ����� ������ k ������.
            if (coroutineWall != null) StopCoroutine(coroutineWall);
            coroutineWall = StartCoroutine(hideWall(PeriodOfLifeOfWall));
        }
    }

    /// <summary>
    /// �������� ����� ����� waitInSec ������.
    /// </summary>
    IEnumerator hideWall(float waitInSec = 5f)
    {
        yield return new WaitForSeconds(waitInSec);
        gameObject.SetActive(false);
    }
}
