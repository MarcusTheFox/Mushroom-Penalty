using Core.UnityHooks;
using Enemies.Components;
using Enemies.UI;
using UnityEngine;

namespace Enemies.CoreLogic
{
    public class EnemyMagic : Enemy
    {
        private EnemyCoreComponentsSetup coreSetup;

        public EnemyMagic(UnityEventListener UEL,
            AnimationEventListener AEL,
            InteractableObjectEvents IOE,
            EnemyUI UI,
            Transform enemyTransform,
            Animator animator) : base(UEL, AEL, IOE, UI, enemyTransform, animator)
        {
        }

        public void Initialize()
        {
            coreSetup = new EnemyCoreComponentsSetup(IOE, 100);
            coreSetup.Initialize();
        
            UI.Initialize(coreSetup.Health);
            
            AEL.OnDead += DestroyEnemy;
        
            UEL.OnDestroyEvent.AddListener(OnDestroy);
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