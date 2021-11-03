using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StatisticDisplay : MonoBehaviour
{
    public Text CoinsText;
    public Text CrystalsText;
    public Text MonsterText;

    Coroutine coroutine1;
    Coroutine coroutine2;
    Coroutine coroutine3;

    // Start is called before the first frame update
    void Start()
    {
        Go();
    }

    private void Go()
    {
        CoinsText.text = "";
        CrystalsText.text = "";
        MonsterText.text = "";

        if (coroutine1 != null) StopCoroutine(coroutine1);
        if (coroutine2 != null) StopCoroutine(coroutine2);
        if (coroutine3 != null) StopCoroutine(coroutine3);
        coroutine1 = StartCoroutine(PrintValueCoins());
        coroutine2 = StartCoroutine(PrintValueCrystals());
        coroutine3 = StartCoroutine(PrintValueMonster());
    }

    IEnumerator PrintValueCoins()
    {
        CoinsText.text = "";
        for (int i = 0; i < StatisticCollector.CoinCount; i++)
        {
            CoinsText.text = "x" + i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        CoinsText.text = "x" + StatisticCollector.CoinCount.ToString();
    }
    IEnumerator PrintValueCrystals()
    {
        CrystalsText.text = "";
        for (int i = 0; i < StatisticCollector.CrystalCount; i++)
        {
            CrystalsText.text = "x" + i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        CrystalsText.text = "x" + StatisticCollector.CrystalCount.ToString();
    }
    IEnumerator PrintValueMonster()
    {
        MonsterText.text = "";
        for (int i = 0; i < StatisticCollector.MonsterCount; i++)
        {
            MonsterText.text = "x" + i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        MonsterText.text = "x" + StatisticCollector.MonsterCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
