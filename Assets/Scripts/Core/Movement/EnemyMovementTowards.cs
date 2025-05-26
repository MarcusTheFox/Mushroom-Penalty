using Core.Interfaces;
using UnityEngine;

namespace Core.Movement
{
    public class EnemyMovementTowards : IMovement
    {
        private Transform enemyTransform;
        private float speed;

        public EnemyMovementTowards(Transform enemyTransform, float speed)
        {
            this.enemyTransform = enemyTransform;
            this.speed = speed;
        }

        public Vector3 Move(Vector3 targetPoint)
        {
            if (speed <= 0) return Vector3.zero;
            
            LookAt(targetPoint);
            
            Vector3 direction = (targetPoint - enemyTransform.position).normalized;
            Vector3 movement = direction * speed * Time.deltaTime;
            enemyTransform.position += movement;
            
            return direction;
        }

        private void LookAt(Vector3 targetPoint)
        {
            Vector3 lookTarget = new Vector3(targetPoint.x, enemyTransform.position.y, targetPoint.z);
            enemyTransform.LookAt(lookTarget);
        }
    }
}