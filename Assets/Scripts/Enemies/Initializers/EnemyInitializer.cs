using Core.UnityHooks;
using Enemies.UI;
using UnityEngine;

namespace Enemies.Initializers
{
    public abstract class EnemyInitializer : ObjectInitializer
    {
        protected EnemyUI UI { get; private set; }
        protected AnimationEventListener AEL { get; private set; }
        protected InteractableObjectEvents IOE { get; private set; }
        protected Animator animator { get; private set; }
        protected GameObject player { get; private set; }
        
        protected override void Initialize()
        {
            base.Initialize();
            AEL = GetComponent<AnimationEventListener>();
            IOE = GetComponent<InteractableObjectEvents>();
            animator = GetComponent<Animator>();
            UI = GetComponent<EnemyUI>();

            AEL.OnDead += DestroyEnemy;
            
            player = GameObject.FindGameObjectWithTag("Player");
        
            CreateEnemy();
        }

        protected abstract void CreateEnemy();

        private void DestroyEnemy()
        {
            AEL.OnDead -= DestroyEnemy;
            Destroy(gameObject);
        }
    }
}