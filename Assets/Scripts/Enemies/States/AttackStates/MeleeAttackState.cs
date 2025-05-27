using AI;
using Combat.Interfaces;
using Core.Interfaces;
using Enemies.CoreLogic;
using Enemies.Handlers;
using UnityEngine;

namespace Enemies.States.AttackStates
{
    public class MeleeAttackState : BaseState<Enemy>, ICleanupable
    {
        private readonly IAttack attack;
        private readonly EnemyAnimationController animationController;
        private bool attackReady;

        public MeleeAttackState(IAttack attack, EnemyAnimationController animationController)
        {
            this.attack = attack;
            this.animationController = animationController;

            attackReady = true;
            this.attack.OnReady += SetAttackIsReady;
        }

        public void Cleanup()
        {
            attack.OnReady -= SetAttackIsReady;
        }

        public override void OnUpdate(Enemy context, float deltaTime)
        {
            base.OnUpdate(context, deltaTime);
            context.LookToTarget();
            if (attackReady)
            {
                attackReady = false;
                attack.Start();
                animationController.OnMeleeAttack(true);
            }
        }

        private void SetAttackIsReady()
        {
            attackReady = true;
        }
    }
}