using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public Hero hero;
    public GameObject buttons;
    public GameObject settings;
    public GameObject about;


    public void StartGame()
    {
        // ��������� ���� ���������.
        Settings.Instance.GamePause = false;
        // ��������� ��������� �����.
        hero.Play();
        // 
        Cursor.visible = Settings.Instance.MouseShow;
        // �������� ��������� ����.
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        // ������� ����������
        Application.Quit();    
    }

    public void OpenSettings()
    {
        Cursor.visible = true;
        buttons.SetActive(false);
        settings.SetActive(true);

        Settings set = Settings.Instance;
        set.TFullScreen.isOn = set.FullScreen;
        set.TMouseShow.isOn = set.MouseShow;
        set.DDListDifficult.value = set.Difficult;
    }

    public void About()
    {
        Cursor.visible = true;
        buttons.SetActive(false);
        about.SetActive(true);
    }
}
