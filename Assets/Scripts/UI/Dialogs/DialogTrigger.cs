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
            hero.Stop();
            TriggerDialog();
        }
    }

    public void TriggerDialog()
    {
        Hero hero = Hero.Instance;
        FindObjectOfType<DialogManager>().StartDialog(dialog);
        hero.Play();
    }
}
