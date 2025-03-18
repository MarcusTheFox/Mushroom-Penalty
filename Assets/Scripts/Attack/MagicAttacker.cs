using UnityEngine;

public class MagicAttacker : MonoBehaviour, IAttacker
{
    public float damage = 15f;
    public float attackRange = 1f;
    private IDamageable owner;

    private void Awake()
    {
        owner = GetComponentInParent<IDamageable>();
        if(owner == null)
        {
            Debug.LogError("No IDamageable found in parents of the Attacker Component");
            enabled = false;
            return;
        }
    }

    public void PerformAttack()
    {
        if (owner is Character attackingCharacter)
        {
            attackingCharacter.PlayAttackAnimation();
        }

        Collider[] hitColliders = Physics.OverlapSphere(
            ((MonoBehaviour)owner).transform.position + ((MonoBehaviour)owner).transform.forward, 
            attackRange);
        foreach (var hitCollider in hitColliders)
        {
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();
            if (damageable != null && hitCollider.gameObject != ((MonoBehaviour)owner).gameObject)
            {
                float finalDamage = DamageCalculator.CalculateDamage(damage, DamageType.Magical);
                damageable.TakeDamage(finalDamage, DamageType.Magical);
            }
        }
    }
}