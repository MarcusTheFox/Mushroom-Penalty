using UnityEngine;

public class FireballAttack : MagicAttack
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireballSpeed = 10f;

     protected override void Awake()
    {
        base.Awake();
        if (fireballPrefab == null)
        {
            Debug.LogError("Fireball prefab not assigned to FireballAttack!");
            enabled = false;
            return;
        }
        if (firePoint == null)
        {
            Debug.LogError("FirePoint not assigned to FireballAttack!");
            enabled = false;
            return;
        }
    }

    public override void PerformAttack()
    {
        if (IsOnCooldown) return;
        
        PlayAttackAnimation();

        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Fireball fireballComponent = fireball.GetComponent<Fireball>();

        if (fireballComponent != null)
        {
            fireballComponent.Initialize(owner, firePoint.forward, damage);
        }
        else
        {
            Debug.LogError("Fireball Prefab does not have Fireball Component!", fireballPrefab);
        }

        cooldownSystem.StartCooldown();
    }
}
