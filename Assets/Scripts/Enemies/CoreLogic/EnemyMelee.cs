using AI.StateMachine;
using Core.UnityHooks;
using Enemies.Components;
using Enemies.UI;
using UnityEngine;

namespace Enemies.CoreLogic
{
    public class EnemyMelee
    {
        private AnimationEventListener AEL;
        private UnityEventListener UEL;
        private EnemyUI UI;
        private InteractableObjectEvents IOE;
        private Transform enemyTransform;
        private Animator animator;
        
        private EnemyCoreComponentsSetup coreSetup;

        public EnemyMelee(AnimationEventListener AEL,
            UnityEventListener UEL,
            InteractableObjectEvents IOE,
            EnemyUI UI,
            Transform enemyTransform,
            Animator animator)
        {
            this.AEL = AEL;
            this.UEL = UEL;
            this.IOE = IOE;
            this.UI = UI;
            this.enemyTransform = enemyTransform;
            this.animator = animator;
        }

        public void Initialize()
        {
            coreSetup = new EnemyCoreComponentsSetup(IOE, 100);
            coreSetup.Initialize();
        
            UI.Initialize(coreSetup.Health);
            
            InitializeStateMachine();
            
            AEL.OnDead += DestroyEnemy;
        
            UEL.OnDestroyEvent.AddListener(OnDestroy);
        }

        private void InitializeStateMachine()
        {
            IStateMachine<EnemyMelee> stateMachine = new StateMachine<EnemyMelee>(this);
        }

        private void OnDestroy()
        {
            UEL.OnDestroyEvent.RemoveListener(OnDestroy);
            
            coreSetup.Cleanup();
            
            UI.Cleanup();
        }

        private void DestroyEnemy()
        {
            AEL.OnDead -= DestroyEnemy;
        }
    }
}