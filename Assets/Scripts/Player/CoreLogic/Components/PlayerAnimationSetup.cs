using Combat.Interfaces;
using Core.Interfaces;
using Player.CoreLogic.Handlers;
using Player.Input;
using UnityEngine;

namespace Player.CoreLogic.Components
{
    public class PlayerAnimationSetup : ICleanupable
    {
        private PlayerAnimationController animationController;
        private Animator animator;
        private PlayerInputController PIC;
        private IDamageable damageable;

        public PlayerAnimationSetup(Animator animator, PlayerInputController PIC, IDamageable damageable)
        {
            this.animator = animator;
            this.PIC = PIC;
            this.damageable = damageable;
        }

        public void Initialize()
        {
            animationController = new PlayerAnimationController(animator);
            AddAnimationInputHandler();
        }

        public void Cleanup()
        {
            RemoveAnimationInputHandler();
        }

        private void AddAnimationInputHandler()
        {
            PIC.Move += animationController.OnMove;
            PIC.Run += animationController.OnRun;
            PIC.MeleeAttack += animationController.OnMeleeAttack;
            PIC.MagicAttack += animationController.OnMagicAttack;
            damageable.OnDeath += animationController.OnDie;
        }

        private void RemoveAnimationInputHandler()
        {
            PIC.Move -= animationController.OnMove;
            PIC.Run -= animationController.OnRun;
            PIC.MeleeAttack -= animationController.OnMeleeAttack;
            PIC.MagicAttack -= animationController.OnMagicAttack;
            damageable.OnDeath -= animationController.OnDie;
        }
    }
}