using UnityEngine;

public class GoToNextLevel : MonoBehaviour
{
    public GameObject NextLevelObject;
    public GameObject PreviousLevelObject;
    public Hero Heroin;
    public GameObject Nebula;
    public GameObject Statistic;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            // 1 ���������� ����������
            Statistic.SetActive(true);
            // 2 ��������� ����
            Nebula.SetActive(false);
            // 3 ��������� �����
            Heroin.Pause();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ChangeLevel()
    {
        // ���������� ������� 2
        NextLevelObject.SetActive(true);

        // �������� ������� 1
        PreviousLevelObject.SetActive(false);

        // �������� ����
        Nebula.SetActive(true);

        // ��������� ����������
        Statistic.SetActive(false);

        // ������������ �����
        Heroin.Play();

    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PreviousLevelObject)
        {
            Debug.LogError("��� ������� �������� � ������ ������ �� ��� ����� ������ ����������� ������!");
        }

        if (!NextLevelObject)
        {
            Debug.LogError("��� ������� �������� � ������ ������ �� ��� ����� ������ ���������� ������!");
        }

        if (Heroin == null)
        {
            Debug.LogError("��� ������� �������� � ������ ������ �� ��� ����� ������ ��!");
        }
        if (!Nebula)
        {
            Debug.LogError("��� ������� �������� � ������ ������ �� ��� ����� ������ ����!");
        }
        if (!Statistic)
        {
            Debug.LogError("��� ������� �������� � ������ ������ �� ��� ����� ������ ����������!");
        }
        if (!Statistic)
        {
            Debug.LogError("��� ������� �������� � ������ ������ �� ��� ����� ������ ����������!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
