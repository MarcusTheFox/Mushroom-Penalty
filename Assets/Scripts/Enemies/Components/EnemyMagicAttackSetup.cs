using Combat.Handlers;
using Combat.Implementations;
using Core.Interfaces;
using Core.UnityHooks;
using Enemies.Data;
using UnityEngine;

namespace Enemies.Components
{
    public class EnemyMagicAttackSetup : ICleanupable
    {
        private readonly UnityEventListener UEL;
        private readonly AnimationEventListener AEL;
        private readonly Transform enemyTransform;
        private readonly Transform fireballSpawnPoint;
        private readonly EnemyMagicDataSO data;

        private MagicAttack magicAttack;
        private Cooldown cooldown;
        private MagicAttackAnimationEventHandler attackAnimationEventHandler;

        public EnemyMagicAttackSetup(UnityEventListener UEL,
            AnimationEventListener AEL,
            Transform enemyTransform,
            Transform fireballSpawnPoint,
            EnemyMagicDataSO data)
        {
            this.UEL = UEL;
            this.AEL = AEL;
            this.enemyTransform = enemyTransform;
            this.fireballSpawnPoint = fireballSpawnPoint;
            this.data = data;
        }

        public void Initialize()
        {
            cooldown = new Cooldown(data.magicCooldown);
            magicAttack = new MagicAttack(data.magicDamage, cooldown, data.magicProjectilePrefab,
                fireballSpawnPoint, enemyTransform);

            attackAnimationEventHandler = new MagicAttackAnimationEventHandler(magicAttack);
            
            UEL.OnUpdateEvent.AddListener(cooldown.Update);
            AddAttackAnimationEventHandler();
        }

        public void Cleanup()
        {
            UEL.OnUpdateEvent.RemoveListener(cooldown.Update);
            cooldown.ClearAndInvalidate();
            
            RemoveAttackAnimationEventHandler();
        }

        private void AddAttackAnimationEventHandler()
        {
            AEL.OnApplyMagicAttack += attackAnimationEventHandler.ApplyMagicAttack;
            AEL.OnStopMagicAttack += attackAnimationEventHandler.StopMagicAttack;
        }

        private void RemoveAttackAnimationEventHandler()
        {
            AEL.OnApplyMagicAttack -= attackAnimationEventHandler.ApplyMagicAttack;
            AEL.OnStopMagicAttack -= attackAnimationEventHandler.StopMagicAttack;
        }
    }
}