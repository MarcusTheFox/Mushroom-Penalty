using UnityEngine;

public class BossStrongAttack : BaseAttack
{
    [SerializeField] private Transform target;
    [SerializeField] private DamageType damageType = DamageType.Fire;
    [SerializeField] private ParticleSystem strongAttackVFX;

    public override void PerformAttack()
    {
        if (IsOnCooldown || target == null) return;

        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, damageType);
            Debug.Log($"BossStrongAttack: нанесён {damage} урон ({damageType}) цели: {target.name}");
        }

        if (strongAttackVFX != null)
        {
            strongAttackVFX.Play();
        }

    }
}
