using Controller;
using UnityEngine;

namespace View
{
    public class EnemyMagic
    {
        public AnimationEventListener AEL;
        public UnityEventListener UEL;
        public Transform enemyTransform;
        public EnemyUI UI;
        public Animator Animator;
        
        private EnemyCoreComponentsSetup coreSetup;
        public InteractableObjectEvents IOE;

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