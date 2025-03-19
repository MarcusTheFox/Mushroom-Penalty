using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeleeAttack : BaseAttack
{
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float attackAngle = 90f;
    [SerializeField] private LayerMask enemyLayers;

    [Tooltip("Visualize the attack range in the editor")]
    [SerializeField] private bool visualizeAttack = true;

    public override void PerformAttack()
    {
        if (IsOnCooldown) return;
        
        Collider[] hitEnemies = FindEnemies();

        foreach (Collider enemyCollider in hitEnemies)
        {
            IDamageable damageable = enemyCollider.GetComponent<IDamageable>();
            if (damageable != null && damageable != owner)
            {
                damageable.TakeDamage(damage, DamageType.Physical);
            }
        }
        
        cooldownSystem.StartCooldown();
    }
    
    private Collider[] FindEnemies()
    {
        Collider[] allColliders = Physics.OverlapSphere(transform.position, attackRadius, enemyLayers);
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

        if (distanceToTargetSqr > attackRadius * attackRadius)
        {
            return false;
        }

        float dotProduct = Vector3.Dot(transform.forward, directionToTarget.normalized);
        float cosHalfAngle = Mathf.Cos(Mathf.Deg2Rad * attackAngle / 2);
        return dotProduct >= cosHalfAngle;
    }
    
    private void OnDrawGizmosSelected()
    {
        if (!visualizeAttack) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Quaternion leftRayRotation = Quaternion.AngleAxis(-attackAngle / 2, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(attackAngle / 2, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, leftRayDirection * attackRadius);
        Gizmos.DrawRay(transform.position, rightRayDirection * attackRadius);

        Handles.color = new Color(1, 1, 0, 0.2f);
        Handles.DrawSolidArc(transform.position, Vector3.up, leftRayDirection, attackAngle, attackRadius);
    }
}