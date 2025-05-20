using System;
using UnityEngine;

public class PlayerAnimationEventListener : MonoBehaviour
{
    public event Action OnApplyMeleeAttack;
    public event Action OnApplyMagicAttack;
    public event Action OnStopMeleeAttack;
    public event Action OnStopMagicAttack;
    public event Action OnDead;

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

    public void PlayerDead()
    {
        OnDead?.Invoke();
    }
}
