using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentence;


    // Start is called before the first frame update
    void Start()
    {
        sentence = new Queue<string>();
    }
    public void StartDialog(Dialog dialog)
    {
        Debug.Log("Talk to: " + dialog.name);
    }
}
