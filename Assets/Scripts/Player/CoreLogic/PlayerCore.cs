using Core.UnityHooks;
using Player.CoreLogic.Components;
using Player.Input;
using Player.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Player.CoreLogic
{
    public class PlayerCore
    {
        private readonly PlayerInputController PIC;
        private readonly AnimationEventListener AEL;
        private readonly UnityEventListener UEL;
        private readonly PlayerUI UI;
        private readonly InteractableObjectEvents IOE;
        private readonly Transform playerTransform;
        private readonly Animator Animator;
        private readonly Transform fireballSpawnPoint;
        private readonly PlayerDataSO playerData;

        private PlayerCoreComponentsSetup coreSetup;
        private PlayerMovementSetup movementSetup;
        private PlayerAttackSetup attackSetup;
        private PlayerAnimationSetup animationSetup;

        public PlayerCore(PlayerInputController PIC,
            AnimationEventListener AEL,
            UnityEventListener UEL,
            InteractableObjectEvents IOE,
            PlayerUI UI,
            Transform playerTransform,
            Animator animator,
            Transform fireballSpawnPoint,
            PlayerDataSO playerData)
        {
            this.PIC = PIC;
            this.AEL = AEL;
            this.UEL = UEL;
            this.IOE = IOE;
            this.UI = UI;
            this.playerTransform = playerTransform;
            Animator = animator;
            this.fireballSpawnPoint = fireballSpawnPoint;
            this.playerData = playerData;
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
