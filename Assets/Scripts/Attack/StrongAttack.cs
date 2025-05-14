using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class StrongAttack : MonoBehaviour, IStrongAttack
{
    [Min(0)]
    [SerializeField] protected float damage = 20f;
    [Min(0)]
    [SerializeField] protected float cooldown;
    private CooldownSystem cooldownSystem;
    private IDamageable owner;
    private AnimationController animationController;

    [SerializeField] private float attackRange = 15f;
    [SerializeField] private float attackAngle = 90f;
    [SerializeField] private LayerMask enemyLayers;


    private void Awake()
    {
        cooldownSystem = new CooldownSystem(cooldown);
        owner = GetComponentInParent<IDamageable>();
        animationController = GetComponentInChildren<AnimationController>();
        if (owner == null)
        {
            Debug.LogWarning("No IDamageable found in parents of the Attacker Component!", this);
        }
    }

    private void Update()
    {
        cooldownSystem.Update(Time.deltaTime);
    }

    public void PerformAttack()
    {
        if (IsOnCooldown) return;

        cooldownSystem.StartCooldown();

        animationController.PlayStrongAttackAnimation();
    }

    public bool IsOnCooldown => cooldownSystem.IsOnCooldown;
    public float GetCooldownProgress() => cooldownSystem.GetCooldownProgress();
    public float AttackRange => attackRange;

    public virtual void StrongAttackEvent()
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
}
