using Combat.Handlers;
using Combat.Implementations;
using Core.Interfaces;
using Core.UnityHooks;
using Enemies.Components.Data;
using UnityEngine;

namespace Enemies.Components
{
    public class EnemyMagicAttackSetup : ICleanupable
    {
        public MagicAttack MagicAttack { get; private set; }
        
        private readonly UnityEventListener UEL;
        private readonly AnimationEventListener AEL;
        private readonly Transform enemyTransform;
        private readonly Transform fireballSpawnPoint;
        private readonly float magicCooldown;
        private readonly float magicDamage;
        private readonly GameObject magicProjectilePrefab;

        private Cooldown cooldown;
        private MagicAttackAnimationEventHandler attackAnimationEventHandler;

        public EnemyMagicAttackSetup(MagicSetupData data)
        {
            UEL = data.UEL;
            AEL = data.AEL;
            enemyTransform = data.EnemyTransform;
            fireballSpawnPoint = data.FireballSpawnPoint;
            magicCooldown = data.Cooldown;
            magicDamage = data.Damage;
            magicProjectilePrefab = data.ProjectilePrefab;
        }

        public void Initialize()
        {
            cooldown = new Cooldown(magicCooldown);
            MagicAttack = new MagicAttack(magicDamage, cooldown, magicProjectilePrefab,
                fireballSpawnPoint, enemyTransform);

            attackAnimationEventHandler = new MagicAttackAnimationEventHandler(MagicAttack);
            
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