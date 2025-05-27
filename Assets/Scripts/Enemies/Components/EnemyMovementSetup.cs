using Core.Interfaces;
using Core.Movement;
using Enemies.Data;
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

        public EnemyMovementSetup(Transform enemyTransform, EnemyDataSO data)
        {
            this.enemyTransform = enemyTransform;
        }

        public void Initialize()
        {
            EnemyMovementTowards = new EnemyMovementTowards(enemyTransform, speedTowards);
            EnemyMovementAway = new EnemyMovementAway(enemyTransform, speedAway);
        }
    }
}