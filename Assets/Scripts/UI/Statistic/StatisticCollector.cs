using UnityEngine;
using UnityEngine.UI;

public class StatisticCollector : MonoBehaviour
{
    public static int CoinCount;
    public static int CrystalCount;
    public static int MonsterCount;

    public Text CointText;
    public Text CrystalText;
    //public Text MonsterText;

    // Start is called before the first frame update
    void Start()
    {
        //Text.color = Color.white;
        CoinCount = 0;
        CrystalCount = 0;
        MonsterCount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        CointText.text = "x" + CoinCount.ToString();
        CrystalText.text = "x" + CrystalCount.ToString();
        //MonsterText.text = "x" + MonsterCount.ToString();
    }
}

