using Combat.Interfaces;
using Enemies.Handlers;
using UnityEngine;

namespace Enemies.States.AttackStates
{
    public class ExplosionState : AttackState
    {
        public ExplosionState(IAttack attack, EnemyAnimationController animationController, Transform enemyTransform, Transform target) : base(attack, animationController, enemyTransform, target)
        {
        }

        protected override void StartAnimation()
        {
            animationController.OnExplode(true);
        }
    }
}