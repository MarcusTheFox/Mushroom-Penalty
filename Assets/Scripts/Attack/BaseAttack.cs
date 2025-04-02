using UnityEngine;

public abstract class BaseAttack : MonoBehaviour, IAttack
{
    [Min(0)]
    [SerializeField] protected float damage = 10f;
    [Min(0)]
    [SerializeField] protected float cooldown;
    protected CooldownSystem cooldownSystem;
    protected IDamageable owner;
    protected AnimationController animationController;

    protected virtual void Awake()
    {
        cooldownSystem = new CooldownSystem(cooldown);
        owner = GetComponentInParent<IDamageable>();
        animationController = GetComponentInChildren<AnimationController>();
        if (owner == null)
        {
            Debug.LogWarning("No IDamageable found in parents of the Attacker Component!", this);
        }
    }

    protected virtual void Update()
    {
        cooldownSystem.Update(Time.deltaTime);
    }

    public virtual void PerformAttack()
    {
        if (IsOnCooldown) return;
        
        cooldownSystem.StartCooldown();
    }

    public bool IsOnCooldown => cooldownSystem.IsOnCooldown;
    public float GetCooldownProgress() => cooldownSystem.GetCooldownProgress();
}
