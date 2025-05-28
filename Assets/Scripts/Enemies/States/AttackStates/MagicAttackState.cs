using Combat.Interfaces;
using Enemies.Handlers;

namespace Enemies.States.AttackStates
{
    public class MagicAttackState : AttackState
    {
        public MagicAttackState(IAttack attack, EnemyAnimationController animationController) : base(attack, animationController)
        {
        }

        protected override void StartAnimation()
        {
            animationController.OnMagicAttack(true);
        }
    }
}