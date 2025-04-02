using System;
using UnityEditor;
using UnityEngine;

public class Enemy : Character
{
    [Header("AI Ranges")]
    [Min(0)]
    [SerializeField] private float viewDistance = 5f;
    [Min(0)]
    [SerializeField] private float attackRange = 3f;
    [Min(0)]
    [SerializeField] private float stopMoveDistance = 2f;
    [Min(0)]
    [SerializeField] private float fleeDistance = 1f;

    [Header("Visualization")]
    [SerializeField] private bool visualizeRanges;
    [SerializeField] private bool alwaysVisualizeRanges;
    
    public float ViewDistance => viewDistance;
    public float AttackRange => attackRange;
    public float StopMoveDistance => stopMoveDistance;
    public float FleeDistance => fleeDistance;
    
    private Transform playerTransform;
    private IAttack attack;
    private IMovable movement;
    private EnemyStateMachine stateMachine;

    private bool canAttack = true;
    private bool canMove = true;

    protected override void Awake()
    {
        base.Awake();
        GameObject playerObjectWithTag = GameObject.FindGameObjectWithTag("Player");
        if (!playerObjectWithTag)
        {
            Debug.LogError("No player object with the tag 'Player'", this);
            enabled = false;
            return;
        }
        
        playerTransform = playerObjectWithTag.transform;
        attack = GetComponent<IAttack>();
        movement = GetComponent<IMovable>();
        if (movement == null)
        {
            Debug.LogWarning("No IMovement found on Enemy!");
        }
        if (attack == null)
        {
            Debug.LogError("No IAttack found on Enemy!");
            enabled = false;
            return;
        }

        stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        stateMachine.Initialize(stateMachine.IdleState);
    }

    private void Update()
    {
        if (!playerTransform) return;
        
        stateMachine.Update();
    }

    public float GetDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, playerTransform.position);
    }

    public void LookToPlayer()
    {
        Vector3 lookTarget = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.LookAt(lookTarget);
    }
    
    public void LookAwayFromPlayer()
    {
        if (!playerTransform) return;
        Vector3 lookDirection = transform.position - playerTransform.position;
        lookDirection.y = transform.position.y;
        transform.LookAt(lookDirection);
    }

    public void MoveToPlayer()
    {
        if (!canMove) return;
        
        LookToPlayer();
        
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        movement.Move(directionToPlayer);
    }
    
    public void FleeFromPlayer()
    {
        if (!canMove) return;

        LookAwayFromPlayer();

        Vector3 directionAwayFromPlayer = (transform.position - playerTransform.position).normalized;
        movement.Move(directionAwayFromPlayer);
    }

    public void StopMoving()
    {
        movement.Stop();
    }

    public void AttackPlayer()
    {
        LookToPlayer();
        
        if (canAttack)
        {
            attack.PerformAttack();
            
            canAttack = false;
        }
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    public void AttackEndEvent()
    {
        canAttack = true;
        EnableMovement();
    }

    protected override void Die()
    {
        animationController?.PlayDeathAnimation();
    }

    public void DeadEvent()
    {
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!visualizeRanges) return;
        if (alwaysVisualizeRanges)
            DrawGizmos();
    }

    private void OnDrawGizmosSelected()
    {
        if (!visualizeRanges) return;
        if (!alwaysVisualizeRanges)
            DrawGizmos();
    }
    
    private void DrawGizmos()
    {
        Vector3 position = transform.position;
        Vector3 normal = Vector3.up;

        Color attackColor = new Color(1f, 0f, 0f, 0.15f);
        Color stopColor = new Color(1f, 1f, 0f, 0.15f);
        Color viewColor = new Color(0f, 1f, 0f, 0.1f);
        Color fleeColor = new Color(0f, 0f, 1f, 0.1f);

        Color originalColor = Handles.color;

        Handles.color = viewColor;
        Handles.DrawSolidDisc(position, normal, ViewDistance);

        Handles.color = attackColor;
        Handles.DrawSolidDisc(position, normal, AttackRange);

        Handles.color = stopColor;
        Handles.DrawSolidDisc(position, normal, StopMoveDistance);
        
        Handles.color = fleeColor;
        Handles.DrawSolidDisc(position, normal, FleeDistance);
        
        Handles.color = Color.red * 0.8f;
        Handles.DrawWireDisc(position, normal, AttackRange);

        Handles.color = Color.yellow * 0.8f;
        Handles.DrawWireDisc(position, normal, StopMoveDistance);

        Handles.color = Color.green * 0.8f;
        Handles.DrawWireDisc(position, normal, ViewDistance);

        Handles.color = Color.blue * 0.8f;
        Handles.DrawWireDisc(position, normal, FleeDistance);

        try
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer < ViewDistance)
                Handles.color = Color.green * 2f;
            if (distanceToPlayer < AttackRange)
                Handles.color = Color.red * 2f;
            Handles.DrawLine(transform.position, playerTransform.position);
        }
        catch
        {
            // ignored
        }
         
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.MiddleCenter;
        labelStyle.fontStyle = FontStyle.Bold;

        float labelHeightOffset = 0.1f;
        Handles.Label(position + transform.right * AttackRange + normal * labelHeightOffset, "Attack", labelStyle);
        Handles.Label(position - transform.right * StopMoveDistance + normal * labelHeightOffset, "Stop", labelStyle);
        Handles.Label(position + transform.forward * ViewDistance + normal * labelHeightOffset, "View", labelStyle);
        Handles.Label(position - transform.forward * FleeDistance + normal * labelHeightOffset, "Flee", labelStyle);
        
        Handles.color = originalColor;
    }
#endif
}