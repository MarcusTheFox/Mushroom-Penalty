using Core.Interfaces;
using Core.Movement;
using UnityEngine;

namespace Enemies.Components
{
    public class EnemyMovementSetup
    {
        private readonly Transform enemyTransform;
        private readonly float speedTowards;
        private readonly float speedAway;
        public IMovement EnemyMovementTowards { get; private set; }
        public IMovement EnemyMovementAway { get; private set; }

        public EnemyMovementSetup(Transform enemyTransform, float speedTowards, float speedAway)
        {
            this.enemyTransform = enemyTransform;
            this.speedTowards = speedTowards;
            this.speedAway = speedAway;
        }

        public void Initialize()
        {
            EnemyMovementTowards = new EnemyMovementTowards(enemyTransform, speedTowards);
            EnemyMovementAway = new EnemyMovementAway(enemyTransform, speedAway);
        }
    }
}