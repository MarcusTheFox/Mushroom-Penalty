using Core.UnityHooks;
using Player.CoreLogic.Components;
using Player.CoreLogic.Context;
using Player.Data;
using Player.Input;
using Player.UI;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Player.CoreLogic
{
    public class PlayerCore
    {
        private readonly GameOverUIManager gameOverUIManage;
        private readonly PlayerInputController PIC;
        private readonly AnimationEventListener AEL;
        private readonly UnityEventListener UEL;
        private readonly PlayerUI UI;
        private readonly InteractableObjectEvents IOE;
        private readonly Transform playerTransform;
        private readonly Animator Animator;
        private readonly Transform fireballSpawnPoint;

        private PlayerCoreComponentsSetup coreSetup;
        private PlayerMovementSetup movementSetup;
        private PlayerAttackSetup attackSetup;
        private PlayerAnimationSetup animationSetup;
        private PlayerDataSO playerData;

        public PlayerCore(PlayerContext context, GameOverUIManager gameOverUIManage)
        {
            this.gameOverUIManage = gameOverUIManage;
            PIC = context.PIC;
            AEL = context.AEL;
            UEL = context.UEL;
            IOE = context.IOE;
            UI = context.UI;
            playerTransform = context.PlayerTransform;
            Animator = context.Animator;
            fireballSpawnPoint = context.FireballSpawnPoint;
        }

        public void Initialize()
        {
            coreSetup = new PlayerCoreComponentsSetup(PIC, IOE, playerData.health);
            coreSetup.Initialize();

            movementSetup = new PlayerMovementSetup(PIC, UEL, playerTransform, playerData);
            movementSetup.Initialize();
        
            attackSetup = new PlayerAttackSetup(PIC, AEL, UEL, playerTransform, fireballSpawnPoint, playerData);
            attackSetup.Initialize();

            animationSetup = new PlayerAnimationSetup(Animator, PIC, coreSetup.Damageable);
            animationSetup.Initialize();
        
            UI.Initialize(coreSetup.Health, attackSetup.MagicCooldown);

            AEL.OnDead += DestroyPlayer;
        
            UEL.OnDestroyEvent.AddListener(OnDestroy);
        }

        private void OnDestroy()
        {
            UEL.OnDestroyEvent.RemoveListener(OnDestroy);
        
            coreSetup.Cleanup();
            movementSetup.Cleanup();
            attackSetup.Cleanup();
            animationSetup.Cleanup();
            UI.Cleanup();
        }

        private void DestroyPlayer()
        {
            AEL.OnDead -= DestroyPlayer;
            Object.Destroy(playerTransform.gameObject);
        }
    }
}
