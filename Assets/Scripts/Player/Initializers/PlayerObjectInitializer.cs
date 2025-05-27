using Core.UnityHooks;
using Player.CoreLogic;
using Player.Data;
using Player.Input;
using Player.UI;
using UnityEngine;

namespace Player.Initializers
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerUI))]
    public class PlayerObjectInitializer : ObjectInitializer
    {
        [SerializeField] private Transform fireballSpawnPoint;
        [SerializeField] private PlayerInputController PIC;
        [SerializeField] private PlayerDataSO playerData;
        private AnimationEventListener AEL;
        private Animator animator;
        private InteractableObjectEvents IOE;
        private PlayerUI UI;

        protected override void Initialize()
        {
            base.Initialize();
            AEL = GetComponent<AnimationEventListener>() ?? gameObject.AddComponent<AnimationEventListener>();
            IOE = GetComponent<InteractableObjectEvents>() ?? gameObject.AddComponent<InteractableObjectEvents>();
            animator = GetComponent<Animator>();
            UI = GetComponent<PlayerUI>();

            CreatePlayer();
        }

        private void CreatePlayer()
        {
            var player = new PlayerCore(
                PIC,
                AEL,
                UEL,
                IOE,
                UI,
                transform,
                animator,
                fireballSpawnPoint);
            
            player.Configure(playerData);
            player.Initialize();
        }
    }
}