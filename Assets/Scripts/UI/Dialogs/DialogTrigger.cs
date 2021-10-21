using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    
    /// <summary>
    /// ��������� �� ��, ����� �� ������� ������ ��������.
    /// </summary>
    public bool isReusable = false;

    /// <summary>
    /// ����� �� ������������� �����.
    /// </summary>
    public bool HeroStop = true;

    /// <summary>
    /// ����� �� ������������ ������.
    /// </summary>
    public bool isActive = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive || isReusable)
        {
            Hero hero = Hero.Instance;
            if (collision.gameObject == hero.gameObject)
            {
                if (HeroStop) hero.Stop();
                TriggerDialog();
            }
        }
    }

    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(this, dialog, HeroStop);
    }
}
