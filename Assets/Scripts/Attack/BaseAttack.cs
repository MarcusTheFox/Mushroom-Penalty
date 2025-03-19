using UnityEngine;

public abstract class BaseAttack : MonoBehaviour, IAttacker
{
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float cooldown = 2f;
    protected CooldownSystem cooldownSystem;
    protected IDamageable owner;

    protected virtual void Awake()
    {
        cooldownSystem = new CooldownSystem(cooldown);
        owner = GetComponentInParent<IDamageable>();
        if (owner == null)
        {
            Debug.LogWarning("No IDamageable found in parents of the Attacker Component!", this);
        }
    }

    protected virtual void Update()
    {
        cooldownSystem.Update(Time.deltaTime);
    }

    public abstract void PerformAttack();

    public bool IsOnCooldown => cooldownSystem.IsOnCooldown;
    public float GetCooldownProgress() => cooldownSystem.GetCooldownProgress();
}
