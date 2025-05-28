using Combat.Interfaces;
using Enemies.Handlers;

namespace Enemies.States.AttackStates
{
    public class MeleeAttackState : AttackState
    {
        public MeleeAttackState(IAttack attack, EnemyAnimationController animationController) : base(attack, animationController)
        {
        }

        protected override void StartAnimation()
        {
            animationController.OnMeleeAttack(true);
        }
    }
}