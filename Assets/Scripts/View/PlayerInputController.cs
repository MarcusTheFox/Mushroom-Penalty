using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputController: MonoBehaviour
{
    public event Action<InputValue> Move;
    public event Action<InputValue> Run;
    public event Action<InputValue> MeleeAttack;
    public event Action<InputValue> MagicAttack;
    
    private PlayerInput playerInput;
    
    private bool canMove = true;
    private bool canAttack = true;
    public void EnableMovementInput() { canMove = true; }
    public void DisableMovementInput() { canMove = false; }
    public void EnableAttackInput() { canAttack = true; }
    public void DisableAttackInput() { canAttack = false; }
    
    private void OnMove(InputValue value)
    {
        if (canMove) Move?.Invoke(value);
    }

    private void OnRun(InputValue value)
    {
        if (canMove) Run?.Invoke(value);
    }

    private void OnMeleeAttack(InputValue value)
    {
        if (canAttack) MeleeAttack?.Invoke(value);
    }

    private void OnMagicAttack(InputValue value)
    {
        if (canAttack) MagicAttack?.Invoke(value);
    }
}
