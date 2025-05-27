using Combat.Handlers;
using Combat.Implementations;
using Combat.Interfaces;
using Core.Interfaces;
using Core.UnityHooks;
using Player.CoreLogic.Handlers;
using Player.Data;
using Player.Input;
using UnityEngine;

namespace Player.CoreLogic.Components
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
        private readonly PlayerDataSO data;

        private PlayerAttackInputHandler attackInputHandler;
        private MeleeAttackAnimationEventHandler meleeAttackAnimationEventHandler;
        private MagicAttackAnimationEventHandler magicAttackAnimationEventHandler;

        public PlayerAttackSetup(PlayerInputController PIC, AnimationEventListener AEL, UnityEventListener UEL,
            Transform playerTransform, Transform fireballSpawnPoint, PlayerDataSO data)
        {
            this.PIC = PIC;
            this.AEL = AEL;
            this.UEL = UEL;
            this.playerTransform = playerTransform;
            this.fireballSpawnPoint = fireballSpawnPoint;
            this.data = data;
        }

        public void Initialize()
        {
            MagicCooldown = new Cooldown(data.magicCooldown);
            MeleeAttack = new MeleeAttack(data.meleeDamage, playerTransform, data.targetLayer, data.meleeRange,
                data.meleeAngle);
            MagicAttack = new MagicAttack(data.magicDamage, MagicCooldown, data.magicProjectilePrefab,
                fireballSpawnPoint, playerTransform);
            
            attackInputHandler = new PlayerAttackInputHandler(MeleeAttack, MagicAttack);

            meleeAttackAnimationEventHandler = new MeleeAttackAnimationEventHandler(MeleeAttack);
            magicAttackAnimationEventHandler = new MagicAttackAnimationEventHandler(MagicAttack);

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
            AEL.OnApplyMeleeAttack += meleeAttackAnimationEventHandler.ApplyMeleeAttack;
            AEL.OnApplyMagicAttack += magicAttackAnimationEventHandler.ApplyMagicAttack;
            AEL.OnStopMeleeAttack += meleeAttackAnimationEventHandler.StopMeleeAttack;
            AEL.OnStopMagicAttack += magicAttackAnimationEventHandler.StopMagicAttack;
            AEL.OnStopMeleeAttack += EnableMovementInput;
            AEL.OnStopMagicAttack += EnableMovementInput;
        }

        private void RemoveAttackAnimationEventHandler()
        {
            AEL.OnApplyMeleeAttack -= meleeAttackAnimationEventHandler.ApplyMeleeAttack;
            AEL.OnApplyMagicAttack -= magicAttackAnimationEventHandler.ApplyMagicAttack;
            AEL.OnStopMeleeAttack -= meleeAttackAnimationEventHandler.StopMeleeAttack;
            AEL.OnStopMagicAttack -= magicAttackAnimationEventHandler.StopMagicAttack;
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