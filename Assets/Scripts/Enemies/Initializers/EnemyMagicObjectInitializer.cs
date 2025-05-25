using Core.UnityHooks;
using Enemies.CoreLogic;
using Enemies.UI;
using UnityEngine;

namespace Enemies.Initializers
{
    public class EnemyMagicObjectInitializer : ObjectInitializer
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
            EnemyMagic enemyMelee = new EnemyMagic(AEL, UEL, IOE, UI, transform, animator);

            enemyMelee.Initialize();
        }
    }
}
