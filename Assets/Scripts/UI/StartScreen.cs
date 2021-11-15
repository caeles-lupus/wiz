using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public Hero hero;
    public GameObject buttons;
    public GameObject settings;
    public GameObject about;


    public void StartGame()
    {
        // Разрешаем всем двигаться.
        Settings.Instance.GamePause = false;
        // Разрешаем двигаться герою.
        hero.Play();
        // 
        Cursor.visible = Settings.Instance.MouseShow;
        // Скрываем стартовое окно.
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        // закрыть приложение
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
