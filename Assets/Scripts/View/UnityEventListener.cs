using UnityEngine;
using UnityEngine.Events;

public class UnityEventListener : MonoBehaviour
{
    public UnityEvent OnAwakeEvent = new UnityEvent();
    public UnityEvent OnStartEvent = new UnityEvent();
    public UnityEvent OnUpdateEvent = new UnityEvent();
    public UnityEvent OnFixedUpdateEvent  = new UnityEvent();
    public UnityEvent OnLateUpdateEvent = new UnityEvent();
    public UnityEvent OnDestroyEvent = new UnityEvent();
    public UnityEvent OnEnableEvent = new UnityEvent();
    public UnityEvent OnDisableEvent = new UnityEvent();
    public UnityEvent<Collider> OnTriggerEnterEvent = new UnityEvent<Collider>();
    public UnityEvent<Collider> OnTriggerStayEvent = new UnityEvent<Collider>();
    public UnityEvent<Collider> OnTriggerExitEvent = new UnityEvent<Collider>();

    private void Awake() { OnAwakeEvent.Invoke(); }
    private void Start() { OnStartEvent.Invoke(); }
    private void Update() { OnUpdateEvent.Invoke(); }
    private void FixedUpdate() { OnFixedUpdateEvent.Invoke(); }
    private void LateUpdate() { OnLateUpdateEvent.Invoke(); }
    private void OnDestroy() { OnDestroyEvent.Invoke(); }
    private void OnEnable() { OnEnableEvent.Invoke(); }
    private void OnDisable() { OnDisableEvent.Invoke(); }
    private void OnTriggerEnter(Collider other) { OnTriggerEnterEvent.Invoke(other); }
    private void OnTriggerStay(Collider other) { OnTriggerStayEvent.Invoke(other); }
    private void OnTriggerExit(Collider other) { OnTriggerExitEvent.Invoke(other); }
    
}
