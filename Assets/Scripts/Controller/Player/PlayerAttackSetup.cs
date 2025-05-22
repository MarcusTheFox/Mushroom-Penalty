using Model;
using UnityEditor;
using UnityEngine;

namespace Controller
{
    public class PlayerAttackSetup : ICleanupable
    {
        public ICooldown MagicCooldown { get; private set; }
        public IAttack MeleeAttack { get; private set; }
        public IAttack MagicAttack { get; private set; }

        private readonly PlayerInputController PIC;
        private readonly AnimationEventListener AEL;
        private readonly UnityEventListener UEL;
        private readonly Transform playerTransform;
        private readonly GameObject fireballPrefab;
        private readonly LayerMask targetLayer;
        private readonly Transform fireballSpawnPoint;

        private PlayerAttackInputHandler attackInputHandler;
        private PlayerAttackAnimationEventHandler attackAnimationEventHandler;

        public PlayerAttackSetup(PlayerInputController PIC, AnimationEventListener AEL, UnityEventListener UEL,
            Transform playerTransform, GameObject fireballPrefab, LayerMask targetLayer, Transform fireballSpawnPoint)
        {
            this.PIC = PIC;
            this.AEL = AEL;
            this.UEL = UEL;
            this.playerTransform = playerTransform;
            this.fireballPrefab = fireballPrefab;
            this.targetLayer = targetLayer;
            this.fireballSpawnPoint = fireballSpawnPoint;
        }

        public void Initialize()
        {
            MagicCooldown = new Cooldown(10);
            MeleeAttack = new MeleeAttack(30, playerTransform, targetLayer, 10f, 90f);
            MagicAttack = new MagicAttack(50, MagicCooldown, targetLayer, fireballPrefab, fireballSpawnPoint,
                playerTransform);
            
            attackInputHandler = new PlayerAttackInputHandler(MeleeAttack, MagicAttack);
            attackAnimationEventHandler = new PlayerAttackAnimationEventHandler(MeleeAttack, MagicAttack);

            UEL.OnUpdateEvent.AddListener(MagicCooldown.Update);
            
            AddAttackInputHandler();
            AddAttackAnimationEventHandler();
            AddAttackEventHandler();
        }

        public void Cleanup()
        {
            UEL.OnUpdateEvent.RemoveListener(MagicCooldown.Update);
            MagicCooldown.ClearAndInvalidate();
            
            RemoveAttackInputHandler();
            RemoveAttackAnimationEventHandler();
            RemoveAttackEventHandler();
        }

        private void AddAttackInputHandler()
        {
            PIC.MeleeAttack += attackInputHandler.OnMeleeAttack;
            PIC.MagicAttack += attackInputHandler.OnMagicAttack;
        }

        private void RemoveAttackInputHandler()
        {
            PIC.MeleeAttack -= attackInputHandler.OnMeleeAttack;
            PIC.MagicAttack -= attackInputHandler.OnMagicAttack;
        }

        private void AddAttackAnimationEventHandler()
        {
            AEL.OnApplyMeleeAttack += attackAnimationEventHandler.ApplyMeleeAttack;
            AEL.OnApplyMagicAttack += attackAnimationEventHandler.ApplyMagicAttack;
            AEL.OnStopMeleeAttack += attackAnimationEventHandler.StopMeleeAttack;
            AEL.OnStopMagicAttack += attackAnimationEventHandler.StopMagicAttack;
            AEL.OnStopMeleeAttack += EnableMovementInput;
            AEL.OnStopMagicAttack += EnableMovementInput;
        }

        private void RemoveAttackAnimationEventHandler()
        {
            AEL.OnApplyMeleeAttack -= attackAnimationEventHandler.ApplyMeleeAttack;
            AEL.OnApplyMagicAttack -= attackAnimationEventHandler.ApplyMagicAttack;
            AEL.OnStopMeleeAttack -= attackAnimationEventHandler.StopMeleeAttack;
            AEL.OnStopMagicAttack -= attackAnimationEventHandler.StopMagicAttack;
            AEL.OnStopMeleeAttack -= EnableMovementInput;
            AEL.OnStopMagicAttack -= EnableMovementInput;
        }

        private void AddAttackEventHandler()
        {
            attackInputHandler.OnStartAttack += DisableMovementInput;
            attackInputHandler.OnStartAttack += DisableMeleeAttackInput;
            attackInputHandler.OnStartAttack += DisableMagicAttackInput;

            MeleeAttack.OnStop += EnableMagicAttackInput;
            MagicAttack.OnStop += EnableMeleeAttackInput;
            MeleeAttack.OnReady += EnableMeleeAttackInput;
            MagicAttack.OnReady += EnableMagicAttackInput;
        }

        private void RemoveAttackEventHandler()
        {
            attackInputHandler.OnStartAttack -= DisableMovementInput;
            attackInputHandler.OnStartAttack -= DisableMeleeAttackInput;
            attackInputHandler.OnStartAttack -= DisableMagicAttackInput;

            MeleeAttack.OnStop -= EnableMagicAttackInput;
            MagicAttack.OnStop -= EnableMeleeAttackInput;
            MeleeAttack.OnReady -= EnableMeleeAttackInput;
            MagicAttack.OnReady -= EnableMagicAttackInput;
        }

        private void EnableMovementInput() => PIC.SetMovementInputEnabled(true);
        private void EnableMeleeAttackInput() => PIC.SetMeleeAttackInputEnabled(true);
        private void EnableMagicAttackInput()
        {
            if (MagicCooldown.IsActive) return;
            PIC.SetMagicAttackInputEnabled(true);
        }

        private void DisableMovementInput() => PIC.SetMovementInputEnabled(false);
        private void DisableMeleeAttackInput() => PIC.SetMeleeAttackInputEnabled(false);
        private void DisableMagicAttackInput() => PIC.SetMagicAttackInputEnabled(false);
    }
}