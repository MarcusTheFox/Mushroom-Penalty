using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float minDistance = 1f;
    
    private Transform playerTransform;
    private IAttacker attacker;
    private IMovable movement;

    protected override void Awake()
    {
        base.Awake();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        attacker = GetComponent<IAttacker>();
        movement = GetComponent<IMovable>();
        if(movement == null)
        {
            Debug.LogWarning("No IMovement found on Enemy!");
        }
        if(attacker == null)
        {
            Debug.LogError("No IAttacker found on Enemy!");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (!playerTransform) return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackRange)
        {
            movement.Move(Vector3.zero);
            transform.LookAt(playerTransform);
            AttackPlayer();
        }
        else
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            
            if (distanceToPlayer > minDistance)
            {
                movement.Move(directionToPlayer);
            }
            else
            {
                movement.Move(Vector3.zero);
            }
            transform.LookAt(playerTransform);
        }
    }

    private void AttackPlayer()
    {
        attacker.PerformAttack();
    }

    protected override void Die()
    {
        animationController?.PlayDeathAnimation();
        Destroy(gameObject);
    }
}