using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public Hero hero;

    // Start is called before the first frame update
    void Start()
    {
        // ����� ��� �� ���� �������������, �� � �� ���������.
        // �� � ����� ������ ��� ��������������� ������.
        //hero.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        hero.Play();
        // �������� ��������� ����.
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();    // ������� ����������
    }
}
