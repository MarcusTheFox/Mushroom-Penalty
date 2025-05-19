using UnityEngine;

[RequireComponent(typeof(UnityEventListener))]
public class ObjectInitializer : MonoBehaviour
{
    protected UnityEventListener UEL { get; private set; }
    protected UI UI { get; private set; }
    protected Animator animator;

    private void Start()
    {
        UEL = GetComponent<UnityEventListener>();
        UI = GetComponent<UI>();
        animator = GetComponent<Animator>();
        
        Initialize();
    }

    protected virtual void Initialize() 
    {
    }
}
