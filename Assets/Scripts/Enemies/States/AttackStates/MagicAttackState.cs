using Combat.Interfaces;
using Enemies.Handlers;
using UnityEngine;

namespace Enemies.States.AttackStates
{
    public class MagicAttackState : AttackState
    {
        public MagicAttackState(IAttack attack, EnemyAnimationController animationController, Transform enemyTransform, Transform target) : base(attack, animationController, enemyTransform, target)
        {
        }

        protected override void StartAnimation()
        {
            animationController.OnMagicAttack(true);
        }
    }
}