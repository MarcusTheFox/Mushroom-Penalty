using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputController : MonoBehaviour
    {
        public event Action<Vector2> Move;
        public event Action<bool> Run;
        public event Action<bool> MeleeAttack;
        public event Action<bool> MagicAttack;
        public event Action Menu;

        private PlayerInput playerInput;
        private InputAction moveAction;
        private InputAction runAction;
        private InputAction meleeAttackAction;
        private InputAction magicAttackAction;
        private InputAction menuAction;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            
            moveAction = playerInput.actions["Move"];
            runAction = playerInput.actions["Run"];
            meleeAttackAction = playerInput.actions["MeleeAttack"];
            magicAttackAction = playerInput.actions["MagicAttack"];
            menuAction = playerInput.actions["Menu"];
        }

        private void OnMove(InputValue value)
        {
            Move?.Invoke(value.Get<Vector2>());
        }

        private void OnRun(InputValue value)
        {
            Run?.Invoke(value.isPressed);
        }

        private void OnMeleeAttack(InputValue value)
        {
            MeleeAttack?.Invoke(value.isPressed);
        }

        private void OnMagicAttack(InputValue value)
        {
            MagicAttack?.Invoke(value.isPressed);
        }

        private void OnMenu(InputValue value)
        {
            if (value.isPressed)
                Menu?.Invoke();
        }

        public void SetMovementInputEnabled(bool value)
        {
            if (value)
            {
                moveAction.Enable();
                runAction.Enable();
            }
            else
            {
                moveAction.Disable();
                runAction.Disable();
            }
        }

        public void SetMeleeAttackInputEnabled(bool value)
        {
            if (value) meleeAttackAction.Enable();
            else meleeAttackAction.Disable();
        }

        public void SetMagicAttackInputEnabled(bool value)
        {
            if (value) magicAttackAction.Enable();
            else magicAttackAction.Disable();
        }

        public bool IsMovementEnabled() => moveAction.enabled;
        public bool IsMeleeAttackEnabled() => meleeAttackAction.enabled;
        public bool IsMagicAttackEnabled() => magicAttackAction.enabled;
    }
}
