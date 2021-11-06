using UnityEngine;

public class GoToNextLevel : MonoBehaviour
{
    public Booster booster;
    public Hero hero;
    public GameObject Statistic;
    public GameObject PreviousLevelObject;
    public GameObject PreviuosBackround;
    public GameObject NextLevelObject;
    public GameObject NextBackround;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            // 1 ���������� ����������
            Statistic.SetActive(true);
            // 2 ��������� ����
            PreviuosBackround.SetActive(false);
            //Destroy(PreviuosBackround);
            // 3 ��������� �����
            hero.Pause();
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
        NextBackround.SetActive(true);

        // ��������� ����������
        Statistic.SetActive(false);

        // ��������������� �������� � ����.
        hero.Health = hero.MaxHealth;
        hero.Mana = hero.MaxMana;
        booster.RemovePositiveEffects();

        // ������������ �����
        hero.Play();

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
        if (booster == null)
        {
            Debug.LogError("��� ������� �������� � ������ ������ �� ��� ����� ������ Booster!");
        }
        if (hero == null)
        {
            Debug.LogError("��� ������� �������� � ������ ������ �� ��� ����� ������ ��!");
        }
        if (!PreviuosBackround)
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
