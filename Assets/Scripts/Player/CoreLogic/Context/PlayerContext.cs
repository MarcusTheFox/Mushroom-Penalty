using Core.UnityHooks;
using Player.Data;
using Player.Input;
using Player.UI;
using UnityEngine;

namespace Player.CoreLogic.Context
{
    public readonly struct PlayerContext
    {
        public readonly UnityEventListener UEL;
        public readonly AnimationEventListener AEL;
        public readonly InteractableObjectEvents IOE;
        public readonly PlayerInputController PIC;
        public readonly PlayerUI UI;
        public readonly Transform PlayerTransform;
        public readonly Animator Animator;
        public readonly Transform FireballSpawnPoint;
        public readonly PlayerDataSO Data;

        public PlayerContext(UnityEventListener uel,
            AnimationEventListener ael,
            InteractableObjectEvents ioe,
            PlayerInputController pic,
            PlayerUI ui,
            Transform playerTransform,
            Animator animator,
            Transform fireballSpawnPoint,
            PlayerDataSO data)
        {
            UEL = uel;
            AEL = ael;
            IOE = ioe;
            PIC = pic;
            UI = ui;
            PlayerTransform = playerTransform;
            Animator = animator;
            FireballSpawnPoint = fireballSpawnPoint;
            Data = data;
        }
    }
}