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
        private PlayerInputController PIC;
        private AnimationEventListener AEL;
        private UnityEventListener UEL;
        private PlayerUI UI;
        private InteractableObjectEvents IOE;
        private Transform playerTransform;
        private Animator Animator;

        private LayerMask targetLayer;
        private GameObject fireballPrefab;
        private Transform fireballSpawnPoint;

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
            GameObject fireballPrefab,
            LayerMask targetLayer,
            Transform fireballSpawnPoint)
        {
            this.PIC = PIC;
            this.AEL = AEL;
            this.UEL = UEL;
            this.IOE = IOE;
            this.UI = UI;
            this.playerTransform = playerTransform;
            Animator = animator;
            this.fireballPrefab = fireballPrefab;
            this.targetLayer = targetLayer;
            this.fireballSpawnPoint = fireballSpawnPoint;
        }

        public void Initialize()
        {
            coreSetup = new PlayerCoreComponentsSetup(PIC, IOE, 100);
            coreSetup.Initialize();

            movementSetup = new PlayerMovementSetup(PIC, UEL, playerTransform, 5f, 10f, 10f);
            movementSetup.Initialize();
        
            attackSetup = new PlayerAttackSetup(PIC, AEL, UEL, playerTransform, fireballPrefab, targetLayer, fireballSpawnPoint);
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
