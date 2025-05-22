using UnityEngine;

[RequireComponent(typeof(UnityEventListener))]
public class ObjectInitializer : MonoBehaviour
{
    protected UnityEventListener UEL { get; private set; }

    private void Awake()
    {
        UEL = GetComponent<UnityEventListener>();
        
        Initialize();
    }

    protected virtual void Initialize() 
    {
    }
}
