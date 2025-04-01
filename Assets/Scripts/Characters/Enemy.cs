using UnityEditor;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float stopMoveDistance = 1f;
    [SerializeField] private float viewDistance = 3f;
    [SerializeField] private bool visualizeRanges;
    
    private Transform playerTransform;
    private IAttack attack;
    private IMovable movement;

    private bool canAttack = true;
    private bool canMove = true;
    private Transform moveTarget;

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
    }

    private void Update()
    {
        if (!playerTransform) return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < viewDistance)
        {
            if (distanceToPlayer < attackRange)
            {
                StopMoving();
                AttackPlayer();
            }
            else
            {
                MoveToPlayer();
            }
        }
        else
        {
            StopMoving();
        }
    }

    private void LookToPlayer()
    {
        Vector3 lookTarget = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.LookAt(lookTarget);
    }

    private void MoveToPlayer()
    {
        if (!canMove) return;
        
        LookToPlayer();
        
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        movement.Move(directionToPlayer);
    }

    private void StopMoving()
    {
        movement.Stop();
    }

    private void AttackPlayer()
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
        if (!visualizeRanges) return; // Check the toggle

        Vector3 position = transform.position;
        // Use Vector3.up to draw the disc flat on the XZ plane (ground) regardless of enemy's tilt
        Vector3 normal = Vector3.up;

        // Define semi-transparent colors for better visibility
        // Adjust the alpha (last value) to control transparency (0=invisible, 1=solid)
        Color attackColor = new Color(1f, 0f, 0f, 0.15f);  // Red, semi-transparent
        Color minDistanceColor = new Color(1f, 1f, 0f, 0.15f); // Yellow, semi-transparent
        Color maxDistanceColor = new Color(0f, 1f, 0f, 0.1f);  // Green, slightly more transparent

        // Store original handle color
        Color originalColor = Handles.color;

        // --- Draw Filled Discs ---
        // It's often best to draw larger discs first so smaller ones appear on top,
        // although with transparency, the order matters less visually.

        // Draw Max Distance (Green)
        Handles.color = maxDistanceColor;
        Handles.DrawSolidDisc(position, normal, viewDistance);

        // Draw Attack Range (Red)
        Handles.color = attackColor;
        Handles.DrawSolidDisc(position, normal, attackRange);

        // Draw Min Distance (Yellow)
        Handles.color = minDistanceColor;
        Handles.DrawSolidDisc(position, normal, stopMoveDistance);


        // --- Optional: Draw Outlines for better definition ---
        // You might want to add thin outlines on top of the filled discs

         Handles.color = Color.red * 0.8f; // Darker red outline
         Handles.DrawWireDisc(position, normal, attackRange);

         Handles.color = Color.yellow * 0.8f; // Darker yellow outline
         Handles.DrawWireDisc(position, normal, stopMoveDistance);

         Handles.color = Color.green * 0.8f; // Darker green outline
         Handles.DrawWireDisc(position, normal, viewDistance);

         try
         {
             float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
             if (distanceToPlayer < viewDistance)
                 Handles.color = Color.green * 2f;
             if (distanceToPlayer < attackRange)
                 Handles.color = Color.red * 2f;
             Handles.DrawLine(transform.position, playerTransform.position);
         }
         catch
         {
             // ignored
         }


         // --- Optional: Add Labels ---
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.normal.textColor = Color.white; // Use white for better contrast on colored discs
        labelStyle.alignment = TextAnchor.MiddleCenter;
        labelStyle.fontStyle = FontStyle.Bold;

        float labelHeightOffset = 0.1f;
        Handles.Label(position + transform.right * attackRange + normal * labelHeightOffset, "Attack range", labelStyle);
        Handles.Label(position - transform.right * stopMoveDistance + normal * labelHeightOffset, "Stop moving", labelStyle);
        Handles.Label(position + transform.forward * viewDistance + normal * labelHeightOffset, "Max view", labelStyle);
        
        Handles.color = originalColor;
    }
#endif
}