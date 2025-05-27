using Core.UnityHooks;
using UnityEngine;

namespace Enemies.Components
{
    public class EnemyMagicAttackSetup
    {
        private readonly AnimationEventListener AEL;
        private readonly Animator animator;
        private readonly Transform target;
        private readonly Transform fireballSpawnPoint;

        public EnemyMagicAttackSetup(AnimationEventListener AEL,
            Animator animator,
            Transform target,
            Transform fireballSpawnPoint)
        {
            this.AEL = AEL;
            this.animator = animator;
            this.target = target;
            this.fireballSpawnPoint = fireballSpawnPoint;
        }
    }
}