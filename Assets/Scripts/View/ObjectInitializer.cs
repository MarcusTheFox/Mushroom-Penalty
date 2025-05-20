using UnityEngine;

[RequireComponent(typeof(UnityEventListener))]
public class ObjectInitializer : MonoBehaviour
{
    protected UnityEventListener UEL { get; private set; }
    protected Animator animator;

    private void Start()
    {
        UEL = GetComponent<UnityEventListener>();
        animator = GetComponent<Animator>();
        
        Initialize();
    }

    protected virtual void Initialize() 
    {
    }
}
