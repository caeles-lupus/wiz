using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StatisticCalc : MonoBehaviour
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
        for (int i = 0; i < CoinCollect.CoinCount; i++)
        {
            CoinsText.text = "x" + i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator PrintValueCrystals()
    {
        CrystalsText.text = "";
        for (int i = 0; i < CrystalCollect.CrystalCount; i++)
        {
            CrystalsText.text = "x" + i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator PrintValueMonster()
    {
        MonsterText.text = "";
        for (int i = 0; i < MonsterCollect.MonsterCount; i++)
        {
            MonsterText.text = "x" + i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        MonsterText.text = "x" + MonsterCollect.MonsterCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
