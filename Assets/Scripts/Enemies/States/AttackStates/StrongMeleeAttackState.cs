using Combat.Interfaces;
using Enemies.Handlers;
using UnityEngine;

namespace Enemies.States.AttackStates
{
    public class StrongMeleeAttackState : AttackState
    {
        public StrongMeleeAttackState(IAttack attack, EnemyAnimationController animationController, Transform enemyTransform, Transform target) : base(attack, animationController, enemyTransform, target)
        {
        }

        protected override void StartAnimation()
        {
            animationController.OnStrongMeleeAttack(true);
        }
    }
}