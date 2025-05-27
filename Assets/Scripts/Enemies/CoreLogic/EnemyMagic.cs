using Core.Interfaces;
using Core.Movement;
using Core.UnityHooks;
using Enemies.Components;
using Enemies.Data;
using Enemies.Handlers;
using Enemies.UI;
using UnityEngine;

namespace Enemies.CoreLogic
{
    public class EnemyMagic : Enemy, IConfigurable<EnemyMagicDataSO>
    {
        private readonly Transform fireballSpawnPoint;
        
        private EnemyMagicAttackSetup magicAttackSetup;
        private EnemyAnimationController animationController;
        private EnemyCoreComponentsSetup coreSetup;
        private EnemyMagicAttackSetup attackSetup;
        private EnemyMovementTowards movementTowards;
        private EnemyMovementAway movementAway;
        private EnemyMagicDataSO data;

        public EnemyMagic(UnityEventListener UEL,
            AnimationEventListener AEL,
            InteractableObjectEvents IOE,
            EnemyUI UI,
            Transform enemyTransform,
            Animator animator,
            Transform target,
            Transform fireballSpawnPoint) : base(UEL, AEL, IOE, UI, enemyTransform, animator, target)
        {
            this.fireballSpawnPoint = fireballSpawnPoint;
        }

        public void Configure(EnemyMagicDataSO data)
        {
            this.data = data;
        }

        protected override void InitializeComponents()
        {
            animationController = new EnemyAnimationController(animator);
            
            coreSetup = new EnemyCoreComponentsSetup(IOE, data.health);
            coreSetup.Initialize();
            
            movementTowards = new EnemyMovementTowards(EnemyTransform, data.speedTowards);
            movementAway = new EnemyMovementAway(EnemyTransform, data.speedAway);
            
            magicAttackSetup = new EnemyMagicAttackSetup(UEL, AEL, EnemyTransform, fireballSpawnPoint, data);
            
            UI.Initialize(coreSetup.Health);
        }

        protected override void ConfigureStateMachine()
        {
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            coreSetup.Cleanup();
            
            UI.Cleanup();
        }
    }
}