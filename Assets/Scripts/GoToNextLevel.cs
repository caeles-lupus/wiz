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
            // 1 Отображаем статистику
            Statistic.SetActive(true);
            // 2 Выключаем небо
            PreviuosBackround.SetActive(false);
            //Destroy(PreviuosBackround);
            // 3 Блокируем героя
            hero.Pause();
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
        NextBackround.SetActive(true);

        // Выключаем статистику
        Statistic.SetActive(false);

        // Восстанавливаем здоровье и ману.
        hero.Health = hero.MaxHealth;
        hero.Mana = hero.MaxMana;
        booster.RemovePositiveEffects();

        // разБлокируем героя
        hero.Play();

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
        if (booster == null)
        {
            Debug.LogError("Для скрипта перехода к новому уровню не был задан объект Booster!");
        }
        if (hero == null)
        {
            Debug.LogError("Для скрипта перехода к новому уровню не был задан объект гг!");
        }
        if (!PreviuosBackround)
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
