using UnityEngine;

namespace Enemies.Handlers
{
    public class EnemyAnimationController
    {
        private Animator animator;

        public EnemyAnimationController(Animator animator)
        {
            this.animator = animator;
        }

        public void OnMove(Vector2 value)
        {
            animator.SetBool("Move", value != Vector2.zero);
        }

        public void OnMeleeAttack(bool value)
        {
            if (value) animator.SetTrigger("MeleeAttack");
        }

        public void OnMagicAttack(bool value)
        {
            if (value) animator.SetTrigger("MagicAttack");
        }

        public void OnDie()
        {
            animator.SetTrigger("Death");
        }
    }
}