using UnityEngine;

public class KickAttack : MeleeAttack
{

    [Header("KickAttack Settings")]
    [SerializeField] private float kickForce = 15f;
    [SerializeField] private LayerMask playerLayer;

    public override void PerformAttack()
    {
        if (IsOnCooldown) return;

        cooldownSystem.StartCooldown();

        animationController.PlayKickAttackAnimation();
    }

    public override void MeleeAttackEvent()
    {
        Collider[] hitPlayers = Physics.OverlapSphere(
            transform.position,
            attackRange,
            playerLayer
        );

        foreach (Collider playerCollider in hitPlayers)
        {
            ApplyKick(playerCollider.transform);
            playerCollider.GetComponent<IDamageable>()?.TakeDamage(damage, DamageType.Physical);
        }
    }

    private void ApplyKick(Transform target)
    {
        Vector3 kickDirection = (target.position - transform.position).normalized;
        kickDirection.y = 0.3f;

        Rigidbody targetRb = target.GetComponent<Rigidbody>();
        if (targetRb != null)
        {
            targetRb.AddForce(kickDirection * kickForce,
                ForceMode.Impulse);
        }
    }


}
