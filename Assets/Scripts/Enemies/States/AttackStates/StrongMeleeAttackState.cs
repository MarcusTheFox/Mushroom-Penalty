using Combat.Interfaces;
using Enemies.Handlers;
using UnityEngine;

namespace Enemies.States.AttackStates
{
    public class StrongMeleeAttackState : AttackState
    {
        public StrongMeleeAttackState(IAttack attack, EnemyAnimationController animationController) : base(attack, animationController)
        {
        }

        protected override void StartAnimation()
        {
            Debug.Log("Strong animation");
            animationController.OnStrongMeleeAttack(true);
        }
    }
}