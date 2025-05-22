using System;
using System.Collections.Generic;
using UnityEngine;
using View;

public class MeleeAttack: IAttack
{
    private readonly Transform playerTransform;
    private readonly LayerMask targetLayer;
    private readonly float attackRange;
    private readonly float attackAngle;
    public float Damage { get; }
    public event Action OnReady;
    public event Action OnStart;
    public event Action OnApply;
    public event Action OnStop;

    public MeleeAttack(float damage,
        Transform playerTransform,
        LayerMask targetLayer,
        float attackRange,
        float attackAngle)
    {
        this.playerTransform = playerTransform;
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
        Collider[] allColliders = Physics.OverlapSphere(playerTransform.position, attackRange, targetLayer);
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
        Vector3 directionToTarget = targetPosition - playerTransform.position;
        float distanceToTargetSqr = directionToTarget.sqrMagnitude;

        if (distanceToTargetSqr > attackRange * attackRange)
        {
            return false;
        }

        float dotProduct = Vector3.Dot(playerTransform.forward, directionToTarget.normalized);
        float cosHalfAngle = Mathf.Cos(Mathf.Deg2Rad * attackAngle / 2);
        return dotProduct >= cosHalfAngle;
    }
}
