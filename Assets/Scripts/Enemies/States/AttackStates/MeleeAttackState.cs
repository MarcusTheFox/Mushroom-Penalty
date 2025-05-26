using AI;
using Combat.Interfaces;
using Core.Interfaces;
using Enemies.CoreLogic;
using Enemies.Handlers;

namespace Enemies.States.AttackStates
{
    public class MeleeAttackState : BaseState, ICleanupable
    {
        private readonly IAttack attack;
        private readonly EnemyAnimationController animationController;
        private bool attackReady;

        public MeleeAttackState(IAttack attack, EnemyAnimationController animationController)
        {
            this.attack = attack;
            this.animationController = animationController;

            attackReady = true;
            attack.OnReady += SetAttackIsReady;
        }

        public void Cleanup()
        {
            attack.OnReady -= SetAttackIsReady;
        }

        public override void OnUpdate(EnemyMelee context, float deltaTime)
        {
            base.OnUpdate(context, deltaTime);
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