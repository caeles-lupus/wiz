using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings Instance { get; set; }

    public GameObject StartScreen;
    public GameObject buttons;
    public GameObject settingsWindow;
    public GameObject AboutWindow;

    public TMPro.TMP_Dropdown DDListDifficult;
    public Toggle TFullScreen;
    public Toggle TMouseShow;

    public bool GamePause = true;

    public int Difficult;
    public bool FullScreen = true;
    public bool MouseShow;

    void Awake()
    {
        Instance = this;
        Difficult = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePause)
            {
                // пауза.
                GamePause = false;
                Hero.Instance.Play();

                // 
                Cursor.visible = MouseShow;

                this.StartScreen.SetActive(false);
                this.buttons.SetActive(false);
                this.settingsWindow.SetActive(false);
                this.AboutWindow.SetActive(false);
            }
            else
            {
                // пауза.
                GamePause = true;
                Hero.Instance.Pause();
                Cursor.visible = true;

                this.settingsWindow.SetActive(false);
                this.AboutWindow.SetActive(false);
                this.buttons.SetActive(true);
                this.StartScreen.SetActive(true);
            }
        }
    }

    public void DifficultChanged(int value)
    {
        value = DDListDifficult.value;

        if (value < 0) value = 0;
        if (value > 2) value = 2;

        Difficult = value;
    }

    public void FullScreenChanged(bool value)
    {
        value = TFullScreen.isOn;

        FullScreen = value;
    }

    public void MouseShowChanged(bool value)
    {
        value = TMouseShow.isOn;

        MouseShow = value;
    }

    public void SaveSettings()
    {
        ApplySettings();
        buttons.SetActive(true);
        settingsWindow.SetActive(false);
    }
    public void ApplySettings()
    {
        //TODO: apply changes
        //Cursor.visible = MouseShow;

        Screen.fullScreen = FullScreen;
    }
}
