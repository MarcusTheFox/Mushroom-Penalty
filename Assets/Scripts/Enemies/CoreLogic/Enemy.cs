using Core.Interfaces;
using Core.UnityHooks;
using Enemies.UI;
using UnityEngine;

namespace Enemies.CoreLogic
{
    public abstract class Enemy : ICleanupable
    {
        protected UnityEventListener UEL { get; }
        protected AnimationEventListener AEL { get; }
        protected InteractableObjectEvents IOE  { get; }
        protected EnemyUI UI { get; }
        protected Transform EnemyTransform { get; }
        protected Animator animator { get; }

        protected Enemy(UnityEventListener UEL,
            AnimationEventListener AEL,
            InteractableObjectEvents IOE,
            EnemyUI UI,
            Transform enemyTransform, Animator animator)
        {
            this.UEL = UEL;
            this.AEL = AEL;
            this.IOE = IOE;
            this.UI = UI;
            EnemyTransform = enemyTransform;
            this.animator = animator;
        }

        public void Cleanup()
        {
        }
    }
}