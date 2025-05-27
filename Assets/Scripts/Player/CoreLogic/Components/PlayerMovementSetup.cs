using Core.Interfaces;
using Core.Movement;
using Core.UnityHooks;
using Player.CoreLogic.Handlers;
using Player.Data;
using Player.Input;
using UnityEngine;

namespace Player.CoreLogic.Components
{
    public class PlayerMovementSetup : ICleanupable
    {
        private readonly PlayerInputController PIC;
        private readonly UnityEventListener UEL;
        private readonly Transform playerTransform;
        private readonly float speed;
        private readonly float runSpeed;
        private readonly float rotationSpeed;
        
        private IMovement movement;
        private PlayerMovementInputHandler movementInputHandler;

        public PlayerMovementSetup(PlayerInputController PIC, UnityEventListener UEL, Transform playerTransform,
            PlayerDataSO playerData)
        {
            this.PIC = PIC;
            this.UEL = UEL;
            this.playerTransform = playerTransform;
            speed = playerData.speed;
            runSpeed = playerData.runSpeed;
            rotationSpeed = playerData.rotationSpeed;
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