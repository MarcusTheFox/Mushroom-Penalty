using UnityEngine;

public class MeleeAttack : Attack
{
    public MeleeAttack(IDamageable attacker, float damage) : base(attacker, damage) { }

    public override void Execute()
    {
        attacker.PlayAttackAnimation();

        Collider[] hitColliders = Physics.OverlapSphere(
            ((MonoBehaviour)attacker).transform.position + ((MonoBehaviour)attacker).transform.forward, 
            1f);
        foreach (var hitCollider in hitColliders)
        {
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();
            if (damageable != null && hitCollider.gameObject != ((MonoBehaviour)attacker).gameObject)
            {
                float finalDamage = DamageCalculator.CalculateDamage(damage, DamageType.Physical);
                damageable.TakeDamage(finalDamage, DamageType.Physical);
            }
        }
    }
}