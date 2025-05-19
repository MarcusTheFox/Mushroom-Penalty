using System;
using UnityEngine;

public class PlayerAttackAnimationEventHandler
{
    private IAttack meleeAttack;
    private IAttack magicAttack;
    
    public event Action<IAttack> onApplyAttack;
    public event Action<IAttack> onStopAttack;

    public PlayerAttackAnimationEventHandler(IAttack meleeAttack, IAttack magicAttack)
    {
        this.meleeAttack = meleeAttack;
        this.magicAttack = magicAttack;
    }

    public void ApplyMeleeAttack()
    {
        onApplyAttack?.Invoke(meleeAttack);
    }

    public void ApplyMagicAttack()
    {
        onApplyAttack?.Invoke(magicAttack);
    }

    public void StopMeleeAttack()
    {
        onStopAttack?.Invoke(meleeAttack);
    }

    public void StopMagicAttack()
    {
        onStopAttack?.Invoke(magicAttack);
    }
}
