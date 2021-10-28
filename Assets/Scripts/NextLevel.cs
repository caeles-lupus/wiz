using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public GameObject level2;
    public GameObject level1;
    public Hero Heroin;
    public GameObject Nebula;
    public GameObject Statisitc;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            // 1 ���������� ����������
            Statisitc.SetActive(true);
            // 2 ��������� ����
            Nebula.SetActive(false);
            // 3 ��������� �����
            Heroin.Pause();
            //// 4 �������� ������� 1
            //level1.SetActive(false);

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ChangeLevel()
    {
        level2.SetActive(true);

        //Destroy(level1);

        // �������� ����
        Nebula.SetActive(true);

        // ��������� ����������
        Statisitc.SetActive(false);

        // ������������ �����
        Heroin.Play();

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
