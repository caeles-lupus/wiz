using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public Hero hero;

    // Start is called before the first frame update
    void Start()
    {
        // Здесь его не надо останавливать, да и не получится.
        // Он в своем срипте сам останавливается теперь.
        //hero.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        hero.Play();
        // Скрываем стартовое окно.
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();    // закрыть приложение
    }
}
