using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float moveSpeed = 3f;
    private float lastAttackTime;
    private Transform playerTransform;

    protected override void Awake()
    {
        base.Awake();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (playerTransform)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= attackRange)
            {
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    AttackPlayer();
                }
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
        transform.LookAt(playerTransform);
        animationController?.PlayMoveAnimation(true, false);
    }

    private void AttackPlayer()
    {
        Debug.Log("Attacking");
        lastAttackTime = Time.time;
        animationController?.PlayAttackAnimation();
        MeleeAttack meleeAttack = new MeleeAttack(this, physicalDamage);
        meleeAttack.Execute();
    }

    protected override void Die()
    {
        animationController?.PlayDeathAnimation();
        Destroy(gameObject, 2f);
    }
}