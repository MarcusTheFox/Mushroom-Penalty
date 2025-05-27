using UnityEngine;

namespace Core.UnityHooks
{
    public class ObjectInitializer : MonoBehaviour
    {
        protected UnityEventListener UEL { get; private set; }

        private void Awake()
        {
            UEL = GetComponent<UnityEventListener>() ?? gameObject.AddComponent<UnityEventListener>();
        
            Initialize();
        }

        protected virtual void Initialize() 
        {
        }
    }
}
