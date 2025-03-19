using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private MeleeAttack meleeAttack;
    private MagicAttack magicAttack;
    
    private IMovable movement;
    private Vector2 moveInput;
    private bool runInput;
    private bool meleeAttackInput;
    private bool magicAttackInput;

    private void Awake()
    {
        meleeAttack = GetComponent<MeleeAttack>();
        magicAttack = GetComponent<MagicAttack>();
        if(magicAttack == null)
        {
            Debug.LogWarning("MagicAttack not found on player");
        }
        
        movement = GetComponent<IMovable>();
        if (movement == null)
        {
            Debug.LogError("No IMovable found on Player!");
            enabled = false;
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnRun(InputValue value)
    {
        runInput = value.isPressed;
    }

    public void OnMeleeAttack(InputValue value)
    {
        meleeAttackInput = value.isPressed;
    }

    public void OnMagicAttack(InputValue value)
    {
        magicAttackInput = value.isPressed;
    }


    private void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        if (moveInput.magnitude > 0)
        {
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            movement.Move(moveDirection);

            if (movement is PlayerMovement playerMovement)
            {
                playerMovement.SetRunning(runInput);
            }
        }
        else
        {
            movement.Stop();
        }
    }

    private void HandleAttack()
    {
        if (meleeAttackInput)
        {
            if(meleeAttack != null)
                meleeAttack.PerformAttack();
            meleeAttackInput = false;
        }
        else if (magicAttackInput)
        {
            if(magicAttack != null)
                magicAttack.PerformAttack();
            magicAttackInput = false;
        }
    }
}