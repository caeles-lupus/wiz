using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class About : MonoBehaviour
{
    public GameObject buttons;


    public void OnClickInAboutWindow()
    {
        //Cursor.visible = Settings.Instance.MouseShow;
        buttons.SetActive(true);
        gameObject.SetActive(false);
    }
}
