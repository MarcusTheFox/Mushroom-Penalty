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
    private bool canRun = true;
    private bool canMelee = true;
    private bool canMagic = true;

    public void EnableMovementInput() { canMove = true; }
    public void DisableMovementInput() { canMove = false; }
    public void EnableMeleeInput() { canMelee = true; }
    public void DisableMeleeInput() { canMelee = false; }
    public void EnableMagicInput() { canMagic = true; }
    public void DisableMagicInput() { canMagic = false; }
    
    private void OnMove(InputValue value)
    {
        if (canMove) Move?.Invoke(value);
    }

    private void OnRun(InputValue value)
    {
        if (canRun) Run?.Invoke(value);
    }

    private void OnMeleeAttack(InputValue value)
    {
        if (canMelee) MeleeAttack?.Invoke(value);
    }

    private void OnMagicAttack(InputValue value)
    {
        if (canMagic) MagicAttack?.Invoke(value);
    }
}
