using AI;
using Core.Interfaces;
using Enemies.CoreLogic;
using Enemies.Handlers;
using UnityEngine;

namespace Enemies.States
{
    public class FleeState : BaseState
    {
        private readonly IMovement movement;
        private readonly EnemyAnimationController animationController;
        private readonly Transform target;

        private bool moving;

        public FleeState(IMovement movement, EnemyAnimationController animationController, Transform target)
        {
            this.movement = movement;
            this.animationController = animationController;
            this.target = target;
        }
        
        public override void OnUpdate(Enemy context, float deltaTime)
        {
            base.OnUpdate(context, deltaTime);
            Vector3 move = movement.Move(target.position);
            move.y = 0;
            bool newMoving = move.magnitude > 0.1f;
            if (newMoving == moving) return;
            moving = newMoving;
            animationController.OnMove(new Vector2(move.x, move.z));
        }

        public override void OnExit(Enemy context)
        {
            base.OnExit(context);
            animationController.OnMove(Vector2.zero);
        }
    }
}