using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackInputHandler
{
    private IAttack meleeAttack;
    private IAttack magicAttack;
    
    public event Action<IAttack> onStartAttack;

    public PlayerAttackInputHandler(IAttack meleeAttack, IAttack magicAttack)
    {
        this.meleeAttack = meleeAttack;
        this.magicAttack = magicAttack;
    }

    public void OnMeleeAttack(InputValue inputValue)
    {
        onStartAttack?.Invoke(meleeAttack);
    }

    public void OnMagicAttack(InputValue inputValue)
    {
        onStartAttack?.Invoke(magicAttack);
    }
}
