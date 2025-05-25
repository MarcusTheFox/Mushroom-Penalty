using Core.Interfaces;
using UnityEngine;

namespace Core.Movement
{
    public class PlayerMovement: IMovement
    {
        private Transform camera;
    
        public PlayerMovement(Transform camera)
        {
            this.camera = camera;
        }
    
        public Vector3 Move(Vector3 direction)
        {
            return GetMoveDirectionByCamera(direction);
        }

        private Vector3 GetMoveDirectionByCamera(Vector3 inputDirection)
        {
            Vector3 cameraForward = camera.forward;
            Vector3 cameraRight = camera.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 direction = cameraForward * inputDirection.z + cameraRight * inputDirection.x;
            return direction.normalized;
        }
    }
}
