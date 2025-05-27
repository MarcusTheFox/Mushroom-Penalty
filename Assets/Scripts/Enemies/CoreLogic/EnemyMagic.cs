using Core.UnityHooks;
using Enemies.UI;
using UnityEngine;

namespace Enemies.CoreLogic
{
    public class EnemyMagic : Enemy
    {

        public EnemyMagic(UnityEventListener UEL,
            AnimationEventListener AEL,
            InteractableObjectEvents IOE,
            EnemyUI UI,
            Transform enemyTransform,
            Animator animator,
            Transform target) : base(UEL, AEL, IOE, UI, enemyTransform, animator, target)
        {
        }

        protected override void InitializeComponents()
        {
            base.InitializeComponents();
        }

        protected override void ConfigureStateMachine()
        {
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}