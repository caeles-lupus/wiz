using UnityEngine;

public class TestMode : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject lvl2;

    public void DoIt()
    {
        StartScreen.SetActive(true);
        lvl2.SetActive(true);
    }
}
