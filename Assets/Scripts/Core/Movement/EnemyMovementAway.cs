using Core.Interfaces;
using UnityEngine;

namespace Core.Movement
{
    public class EnemyMovementAway : IMovement
    {
        private Transform enemyTransform;
        private float speed;

        public EnemyMovementAway(Transform enemyTransform, float speed)
        {
            this.enemyTransform = enemyTransform;
            this.speed = speed;
        }

        public Vector3 Move(Vector3 targetPoint)
        {
            if (speed <= 0) return enemyTransform.position;
            
            LookAt(targetPoint);
            
            Vector3 direction = (enemyTransform.position - targetPoint).normalized;
            Vector3 movement = direction * speed * Time.deltaTime;
            enemyTransform.position += movement;
            return enemyTransform.position;
        }

        private void LookAt(Vector3 targetPoint)
        {
            Vector3 lookTarget = enemyTransform.position - targetPoint;
            lookTarget.y = enemyTransform.position.y;
            enemyTransform.LookAt(lookTarget);
        }
    }
}