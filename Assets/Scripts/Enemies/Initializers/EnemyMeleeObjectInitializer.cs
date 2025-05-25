using Core.UnityHooks;
using Enemies.CoreLogic;
using Enemies.UI;
using UnityEngine;

namespace Enemies.Initializers
{
    public class EnemyMeleeObjectInitializer : ObjectInitializer
    {
        private EnemyUI UI;
        private AnimationEventListener AEL;
        private InteractableObjectEvents IOE;
        private Animator animator;

        protected override void Initialize()
        {
            base.Initialize();
            AEL = GetComponent<AnimationEventListener>();
            IOE = GetComponent<InteractableObjectEvents>();
            animator = GetComponent<Animator>();
            UI = GetComponent<EnemyUI>();
        
            CreateEnemy();
        }

        private void CreateEnemy()
        {
            EnemyMelee enemyMelee = new EnemyMelee(AEL, UEL, IOE, UI, transform, animator);

            enemyMelee.Initialize();
        }
    }
}
