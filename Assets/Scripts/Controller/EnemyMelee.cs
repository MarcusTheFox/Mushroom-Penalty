using UnityEngine;
using View;

namespace Controller
{
    public class EnemyMelee
    {
        public AnimationEventListener AEL;
        public UnityEventListener UEL;
        public Transform enemyTransform;
        public EnemyUI UI;
        public Animator Animator;
        
        private EnemyCoreComponentsSetup coreSetup;

        public void Initialize()
        {
            coreSetup = new EnemyCoreComponentsSetup(100);
            coreSetup.Initialize();
        
            UI.Initialize(coreSetup.Health);
            
            AEL.OnDead += DestroyEnemy;
        
            UEL.OnDestroyEvent.AddListener(OnDestroy);
        }

        private void OnDestroy()
        {
            UEL.OnDestroyEvent.RemoveListener(OnDestroy);
            
            UI.Cleanup();
        }

        private void DestroyEnemy()
        {
            AEL.OnDead -= DestroyEnemy;
        }
    }
}