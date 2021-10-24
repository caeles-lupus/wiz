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

    private bool heroStop = true;
    private DialogTrigger sender;
    private Coroutine coroutine;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialog(DialogTrigger sender, Dialog dialog, bool heroStop = true)
    {
        // Запоминаем триггер, вызвавший менеджера.
        this.sender = sender;
        // Останавливается ли герой.
        this.heroStop = sender.HeroStop;
        // Включаем анимацию появления диалога.
        animator.SetBool("isOne", true);
        //
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
        //dlgText.text = sentence;
        if (coroutine != null) StopCoroutine(coroutine);

        coroutine = StartCoroutine(TypeSentence(sentence));
        
    }

    IEnumerator TypeSentence(string sentence)
    {

        dlgText.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            dlgText.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {
        animator.SetBool("isOne", false);
        if (this.heroStop)
        {
            Hero hero = Hero.Instance;
            hero.Play();
        }
        // Выключаем диалог, чтобы его нельзя было повторно вызвать.
        sender.isActive = false;
    }
}
