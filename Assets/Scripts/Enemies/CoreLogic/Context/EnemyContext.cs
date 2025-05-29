using Core.UnityHooks;
using Enemies.UI;
using UnityEngine;

namespace Enemies.CoreLogic.Context
{
    public readonly struct EnemyContext
    {
        public readonly UnityEventListener UEL;
        public readonly AnimationEventListener AEL;
        public readonly InteractableObjectEvents IOE;
        public readonly EnemyUI UI;
        public readonly Transform EnemyTransform;
        public readonly Animator Animator;
        public readonly Transform Target;

        public EnemyContext(UnityEventListener uel,
            AnimationEventListener ael,
            InteractableObjectEvents ioe,
            EnemyUI ui,
            Transform enemyTransform,
            Animator animator,
            Transform target)
        {
            UEL = uel;
            AEL = ael;
            IOE = ioe;
            UI = ui;
            EnemyTransform = enemyTransform;
            Animator = animator;
            Target = target;
        }
    }
}