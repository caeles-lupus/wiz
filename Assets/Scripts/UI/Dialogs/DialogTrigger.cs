using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    private void OnTriggerEnter2D(Collider2D collision)
    {
           Hero hero = Hero.Instance;
        if (collision.gameObject == hero.gameObject)
        {
            hero.Stopped = true;
            TriggerDialog();
        }
    }

    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }
}
