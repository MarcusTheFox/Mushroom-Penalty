using Core.UnityHooks;
using Enemies.CoreLogic.Context;
using Enemies.UI;
using UnityEngine;

namespace Enemies.Initializers
{
    public abstract class EnemyInitializer : ObjectInitializer
    {
        protected EnemyContext Context { get; private set; }
        private EnemyUI UI;
        private AnimationEventListener AEL;
        private InteractableObjectEvents IOE;
        private Animator animator;
        private GameObject player;
        
        protected override void Initialize()
        {
            base.Initialize();
            AEL = GetComponent<AnimationEventListener>();
            IOE = GetComponent<InteractableObjectEvents>();
            animator = GetComponent<Animator>();
            UI = GetComponent<EnemyUI>();

            AEL.OnDead += DestroyEnemy;
            
            player = GameObject.FindGameObjectWithTag("Player");

            Context = new EnemyContext(
                UEL,
                AEL,
                IOE,
                UI,
                transform,
                animator,
                player.transform
                );
        
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