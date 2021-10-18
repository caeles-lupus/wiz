using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text dlgName;
    public Text dlgText;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialog(Dialog dialog)
    {
        animator.SetBool("isOne", true);
        dlgName.text = dialog.name + ":";
        sentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            this.sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (this.sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = this.sentences.Dequeue();
        dlgText.text = sentence;
    }

    void EndDialog()
    {
        animator.SetBool("isOne", false);
    }
}
