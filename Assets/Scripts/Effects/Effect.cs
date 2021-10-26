using UnityEngine;


public class Effect : MonoBehaviour
{
    [Header("��������� �������")]
    public float TimeOfWorking = 0.3f;

    private void Start()
    {
        
    }

    protected bool isWorked = false;

    public virtual void EffectStart()
    {
        isWorked = true;
    }

    public virtual void EffectStop()
    {
        isWorked = false;
    }
}
