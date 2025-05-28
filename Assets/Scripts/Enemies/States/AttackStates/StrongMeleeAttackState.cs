using Combat.Interfaces;
using Enemies.Handlers;

namespace Enemies.States.AttackStates
{
    public class StrongMeleeAttackState : AttackState
    {
        public StrongMeleeAttackState(IAttack attack, EnemyAnimationController animationController) : base(attack, animationController)
        {
        }

        protected override void StartAnimation()
        {
            animationController.OnStrongMeleeAttack(true);
        }
    }
}