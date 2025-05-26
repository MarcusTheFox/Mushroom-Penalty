using UnityEngine;

public class BossAttack : BaseAttack
{
    [SerializeField] private Transform target;
    [SerializeField] private DamageType damageType = DamageType.Physical;

    public override void PerformAttack()
    {
        if (IsOnCooldown || target == null) return;

        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, damageType);
            Debug.Log($"BossAttack: нанесён {damage} урон ({damageType}) цели: {target.name}");
        }

    }
}
