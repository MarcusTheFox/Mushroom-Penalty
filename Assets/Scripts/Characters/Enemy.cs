using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float minDistance = 1f;
    
    private Transform playerTransform;
    private IAttack attack;
    private IMovable movement;

    protected override void Awake()
    {
        base.Awake();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        attack = GetComponent<IAttack>();
        movement = GetComponent<IMovable>();
        if(movement == null)
        {
            Debug.LogWarning("No IMovement found on Enemy!");
        }
        if(attack == null)
        {
            Debug.LogError("No IAttack found on Enemy!");
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
        attack.PerformAttack();
    }

    protected override void Die()
    {
        animationController?.PlayDeathAnimation();
        Destroy(gameObject);
    }
}