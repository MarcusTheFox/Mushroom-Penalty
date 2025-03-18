using UnityEngine;

public class MagicAttacker : MonoBehaviour, IAttacker
{
    [SerializeField] private float damage = 15f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float cooldown = 2f;
    
    private IDamageable owner;
    private CooldownSystem cooldownSystem;
    public bool IsOnCooldown => cooldownSystem.IsOnCooldown;
    public float GetCooldownProgress() => cooldownSystem.GetCooldownProgress();

    private void Awake()
    {
        owner = GetComponentInParent<IDamageable>();
        if(owner == null)
        {
            Debug.LogError("No IDamageable found in parents of the Attacker Component");
            enabled = false;
            return;
        }
        
        cooldownSystem = new CooldownSystem(cooldown);
    }

    private void Update()
    {
        cooldownSystem.Update(Time.deltaTime);
    }

    public void PerformAttack()
    {
        if (cooldownSystem.IsOnCooldown) return;
        
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
        
        cooldownSystem.StartCooldown();
    }
}