using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class MeleeAttack : BaseAttack
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackAngle = 90f;
    [SerializeField] private LayerMask enemyLayers;

    [Tooltip("Visualize the attack range in the editor")]
    [SerializeField] private bool visualizeAttack = true;

    public float AttackRange => attackRange;
    
    public virtual void MeleeAttackEvent()
    {
        Collider[] hitEnemies = FindEnemies();

        foreach (Collider enemyCollider in hitEnemies)
        {
            IDamageable damageable = enemyCollider.GetComponent<IDamageable>();
            if (damageable != null && damageable != owner)
            {
                damageable.TakeDamage(damage, DamageType.Physical);
            }
        }
    }
    
    private Collider[] FindEnemies()
    {
        Collider[] allColliders = Physics.OverlapSphere(transform.position, attackRange, enemyLayers);
        List<Collider> enemiesInRange = new List<Collider>();

        foreach (Collider col in allColliders)
        {
             if (IsInAttackCone(col.transform.position))
             {
                 enemiesInRange.Add(col);
             }
        }

        return enemiesInRange.ToArray();
    }
    
    private bool IsInAttackCone(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        float distanceToTargetSqr = directionToTarget.sqrMagnitude;

        if (distanceToTargetSqr > attackRange * attackRange)
        {
            return false;
        }

        float dotProduct = Vector3.Dot(transform.forward, directionToTarget.normalized);
        float cosHalfAngle = Mathf.Cos(Mathf.Deg2Rad * attackAngle / 2);
        return dotProduct >= cosHalfAngle;
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!visualizeAttack) return;

        Quaternion leftRayRotation = Quaternion.AngleAxis(-attackAngle / 2, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(attackAngle / 2, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, leftRayDirection * attackRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * attackRange);
        
        Handles.DrawWireDisc(transform.position, Vector3.up, attackRange);

        Handles.color = new Color(1, 1, 0, 0.2f);
        Handles.DrawSolidArc(transform.position, Vector3.up, leftRayDirection, attackAngle, attackRange);
    }
#endif
}