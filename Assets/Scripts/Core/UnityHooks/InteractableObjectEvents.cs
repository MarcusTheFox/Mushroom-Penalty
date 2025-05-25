using System;
using UnityEngine;

namespace Core.UnityHooks
{
    public class InteractableObjectEvents : MonoBehaviour
    {
        public event Action<float> OnDamageAttempt;

        public void AttemptDamage(float damage)
        {
            OnDamageAttempt?.Invoke(damage);
        }
    }
}