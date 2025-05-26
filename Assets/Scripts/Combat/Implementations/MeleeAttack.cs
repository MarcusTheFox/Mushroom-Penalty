using System;
using System.Collections.Generic;
using Combat.Interfaces;
using Core.UnityHooks;
using UnityEngine;

namespace Combat.Implementations
{
    public class MeleeAttack: IAttack
    {
        private readonly Transform fromTransform;
        private readonly LayerMask targetLayer;
        private readonly float attackRange;
        private readonly float attackAngle;
        public float Damage { get; }
        public event Action OnReady;
        public event Action OnStart;
        public event Action OnApply;
        public event Action OnStop;

        public MeleeAttack(float damage,
            Transform fromTransform,
            LayerMask targetLayer,
            float attackRange,
            float attackAngle)
        {
            this.fromTransform = fromTransform;
            this.targetLayer = targetLayer;
            this.attackRange = attackRange;
            this.attackAngle = attackAngle;
            Damage = damage;
        }
    
        public void Ready()
        {
            OnReady?.Invoke();
        }

        public void Start()
        {
            OnStart?.Invoke();
        }

        public void Apply()
        {
            Collider[] hitEnemies = FindEnemies();
            Debug.Log(hitEnemies.Length);
            foreach (Collider collider in hitEnemies)
            {
                InteractableObjectEvents targetIOE = collider.GetComponent<InteractableObjectEvents>();
                targetIOE.AttemptDamage(Damage);
            }
            OnApply?.Invoke();
        }

        public void Stop()
        {
            OnStop?.Invoke();
            Ready();
        }
    
        private Collider[] FindEnemies()
        {
            Collider[] allColliders = Physics.OverlapSphere(fromTransform.position, attackRange, targetLayer);
            List<Collider> targetsInRange = new List<Collider>();
            Debug.Log($"{allColliders.Length} enemies found");
            foreach (Collider col in allColliders)
            {
                if (IsInAttackCone(col.transform.position))
                {
                    targetsInRange.Add(col);
                }
            }

            return targetsInRange.ToArray();
        }
    
        private bool IsInAttackCone(Vector3 targetPosition)
        {
            Vector3 directionToTarget = targetPosition - fromTransform.position;
            float distanceToTargetSqr = directionToTarget.sqrMagnitude;

            if (distanceToTargetSqr > attackRange * attackRange)
            {
                return false;
            }

            float dotProduct = Vector3.Dot(fromTransform.forward, directionToTarget.normalized);
            float cosHalfAngle = Mathf.Cos(Mathf.Deg2Rad * attackAngle / 2);
            return dotProduct >= cosHalfAngle;
        }
    }
}
