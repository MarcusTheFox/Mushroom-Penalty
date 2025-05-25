using Core.Interfaces;
using UnityEngine;

namespace Player.CoreLogic.Handlers
{
    public class PlayerMovementInputHandler
    {
        private IMovement movement;
        private Transform transform;
        private float speed;
        private float runSpeed;
        private float rotationSpeed;
        private bool isRunning;
        private Vector3 inputMoveDirection;

        public PlayerMovementInputHandler(IMovement movement, Transform transform, 
            float speed, float runSpeed, float rotationSpeed = 0)
        {
            this.movement = movement;
            this.transform = transform;
            this.speed = speed;
            this.runSpeed = runSpeed;
            this.rotationSpeed = rotationSpeed;
        }

        public void OnMove(Vector2 value)
        {
            inputMoveDirection = new Vector3(value.x, 0, value.y).normalized;
        }

        public void OnRun(bool value)
        {
            isRunning = value;
        }

        public void OnUpdate()
        {
            Vector3 moveDirection = movement.Move(inputMoveDirection);
            float moveSpeed = isRunning ? runSpeed : speed;
        
            if (moveDirection == Vector3.zero) return;
        
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = rotationSpeed == 0
                ? targetRotation
                : Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
