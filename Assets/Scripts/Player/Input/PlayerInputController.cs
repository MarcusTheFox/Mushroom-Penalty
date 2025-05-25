using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputController: MonoBehaviour
    {
        public event Action<Vector2> Move;
        public event Action<bool> Run;
        public event Action<bool> MeleeAttack;
        public event Action<bool> MagicAttack;
    
        private bool canMove = true;
        private bool canMeleeAttack = true;
        private bool canMagicAttack = true;
    
        private Vector2 tmpMoveInput;
        private bool tmpRunInput;
        private bool tmpMeleeInput;
        private bool tmpMagicInput;
    
        private Vector2 lastMoveInput;
        private bool lastRunInput;
        private bool lastMeleeInput;
        private bool lastMagicInput;
    
        private void OnMove(InputValue value)
        {
            tmpMoveInput = value.Get<Vector2>();
            UpdateMovementState();
        }

        private void OnRun(InputValue value)
        {
            tmpRunInput = value.isPressed;
            UpdateRunState();
        }

        private void OnMeleeAttack(InputValue value)
        {
            tmpMeleeInput = value.isPressed;
            UpdateMeleeAttackState();
        }

        private void OnMagicAttack(InputValue value)
        {
            tmpMagicInput = value.isPressed;
            UpdateMagicAttackState();
        }

        private void UpdateMovementState()
        {
            Vector2 currentMoveInput = canMove ? tmpMoveInput : Vector2.zero;
            if (currentMoveInput == lastMoveInput) return;
            lastMoveInput = currentMoveInput;
        
            Move?.Invoke(currentMoveInput);
        }

        private void UpdateRunState()
        {
            bool currentRunInput = canMove && tmpRunInput;
            if (currentRunInput == lastRunInput) return;
            lastRunInput = currentRunInput;
        
            Run?.Invoke(currentRunInput);
        }

        private void UpdateMeleeAttackState()
        {
            bool currentMeleeInput = canMeleeAttack && tmpMeleeInput;
            if (currentMeleeInput == lastMeleeInput) return;
            lastMeleeInput = currentMeleeInput;
        
            MeleeAttack?.Invoke(currentMeleeInput);
        }

        private void UpdateMagicAttackState()
        {
            bool currentMagicInput = canMagicAttack && tmpMagicInput;
            if (currentMagicInput == lastMagicInput) return;
            lastMagicInput = currentMagicInput;
        
            MagicAttack?.Invoke(currentMagicInput);
        }

        public void SetMovementInputEnabled(bool value)
        {
            if (canMove == value) return;
        
            canMove = value;
            UpdateMovementState();
            UpdateRunState();
        }

        public void SetMeleeAttackInputEnabled(bool value)
        {
            if (canMeleeAttack == value) return;
        
            canMeleeAttack = value;
            UpdateMeleeAttackState();
        }

        public void SetMagicAttackInputEnabled(bool value)
        {
            if (canMagicAttack == value) return;
        
            canMagicAttack = value;
            UpdateMagicAttackState();
        }
    }
}
