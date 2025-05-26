using AI;
using Core.Interfaces;
using Enemies.CoreLogic;
using Enemies.Handlers;
using UnityEngine;

namespace Enemies.States
{
    public class ChaseState : BaseState<Enemy>
    {
        private readonly IMovement movement;
        private readonly EnemyAnimationController animationController;
        
        private bool chasing;

        public ChaseState(IMovement movement, EnemyAnimationController animationController)
        {
            this.movement = movement;
            this.animationController = animationController;
        }
        
        public override void OnUpdate(Enemy context, float deltaTime)
        {
            base.OnUpdate(context, deltaTime);
            Vector3 move = movement.Move(context.Target.position);
            move.y = 0;
            bool newChasing = move.magnitude > 0.1f;
            if (newChasing == chasing) return;
            chasing = newChasing;
            animationController.OnMove(new Vector2(move.x, move.z));
        }

        public override void OnExit(Enemy context)
        {
            base.OnExit(context);
            animationController.OnMove(Vector2.zero);
        }
    }
}