using System;
using UnityEngine;

namespace Core.UnityHooks
{
    public class AnimationEventListener : MonoBehaviour
    {
        public event Action OnApplyMeleeAttack;
        public event Action OnApplyMagicAttack;
        public event Action OnStopMeleeAttack;
        public event Action OnStopMagicAttack;
        public event Action OnDead;
        public event Action OnApplyStrongMeleeAttack;
        public event Action OnStopStrongMeleeAttack;
        public event Action OnApplyExplosion;

        public void ApplyMeleeAttackEvent()
        {
            OnApplyMeleeAttack?.Invoke();
        }
    
        public void StopMeleeAttackEvent()
        {
            OnStopMeleeAttack?.Invoke();
        }
    
        public void ApplyMagicAttackEvent()
        {
            OnApplyMagicAttack?.Invoke();
        }
    
        public void StopMagicAttackEvent()
        {
            OnStopMagicAttack?.Invoke();
        }

        public void DeadEvent()
        {
            OnDead?.Invoke();
        }
        
        public void ApplyStrongMeleeAttackEvent()
        {
            OnApplyStrongMeleeAttack?.Invoke();
        }

        public void StopStrongMeleeAttackEvent()
        {
            OnStopStrongMeleeAttack?.Invoke();
        }

        public void ApplyExplosionEvent()
        {
            OnApplyExplosion?.Invoke();
        }
    }
}
