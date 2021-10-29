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
            // 1 Отображаем статистику
            Statistic.SetActive(true);
            // 2 Выключаем небо
            Nebula.SetActive(false);
            // 3 Блокируем героя
            Heroin.Pause();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ChangeLevel()
    {
        // Показываем уровень 2
        NextLevelObject.SetActive(true);

        // Скрываем уровень 1
        PreviousLevelObject.SetActive(false);

        // Включаем небо
        Nebula.SetActive(true);

        // Выключаем статистику
        Statistic.SetActive(false);

        // разБлокируем героя
        Heroin.Play();

    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PreviousLevelObject)
        {
            Debug.LogError("Для скрипта перехода к новому уровню не был задан объект предыдущего уровня!");
        }

        if (!NextLevelObject)
        {
            Debug.LogError("Для скрипта перехода к новому уровню не был задан объект следующего уровня!");
        }

        if (Heroin == null)
        {
            Debug.LogError("Для скрипта перехода к новому уровню не был задан объект гг!");
        }
        if (!Nebula)
        {
            Debug.LogError("Для скрипта перехода к новому уровню не был задан объект фона!");
        }
        if (!Statistic)
        {
            Debug.LogError("Для скрипта перехода к новому уровню не был задан объект статистики!");
        }
        if (!Statistic)
        {
            Debug.LogError("Для скрипта перехода к новому уровню не был задан объект статистики!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
