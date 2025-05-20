using Model;
using UnityEngine;

namespace Controller
{
    public class PlayerMovementSetup : ICleanupable
    {
        private IMovement movement;
        
        private PlayerInputController PIC;
        private UnityEventListener UEL;
        
        private PlayerMovementInputHandler movementInputHandler;
        
        private Transform playerTransform;
        private float speed;
        private float runSpeed;
        private float rotationSpeed;

        public PlayerMovementSetup(PlayerInputController PIC, UnityEventListener UEL, Transform playerTransform,
            float speed, float runSpeed, float rotationSpeed)
        {
            this.PIC = PIC;
            this.UEL = UEL;
            this.playerTransform = playerTransform;
            this.speed = speed;
            this.runSpeed = runSpeed;
            this.rotationSpeed = rotationSpeed;
        }

        public void Initialize()
        {
            Transform cameraTransform = Camera.main?.transform;
        
            movement = new PlayerMovement(cameraTransform);
            movementInputHandler =
                new PlayerMovementInputHandler(movement, playerTransform, speed, runSpeed, rotationSpeed);

            AddMovementInputHandler();
        }

        public void Cleanup()
        {
            RemoveMovementInputHandler();
        }

        private void AddMovementInputHandler()
        {
            PIC.Move += movementInputHandler.OnMove;
            PIC.Run += movementInputHandler.OnRun;
            UEL.OnUpdateEvent.AddListener(movementInputHandler.OnUpdate);
        }

        private void RemoveMovementInputHandler()
        {
            PIC.Move -= movementInputHandler.OnMove;
            PIC.Run -= movementInputHandler.OnRun;
            UEL.OnUpdateEvent.RemoveListener(movementInputHandler.OnUpdate);
        }
    }
}