using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeedMultiplier = 1.5f;

    private Character character;
    private Rigidbody rb;
    private AnimationController animationController;

    private Vector2 moveInput;
    private bool runInput;
    private bool meleeAttackInput;
    private bool magicAttackInput;

    private void Awake()
    {
        character = GetComponent<Character>();
        rb = GetComponent<Rigidbody>();
        animationController = GetComponent<AnimationController>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on Player!");
            enabled = false;
            return;
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
            float currentSpeed = moveSpeed;
            if (runInput)
            {
                currentSpeed *= runSpeedMultiplier;
            }

            rb.linearVelocity = moveDirection * currentSpeed;

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            animationController?.PlayMoveAnimation(true, runInput);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
            animationController?.PlayMoveAnimation(false, false);
        }
    }

    private void HandleAttack()
    {
        if (meleeAttackInput)
        {
            MeleeAttack meleeAttack = new MeleeAttack(character, character.physicalDamage);
            meleeAttack.Execute();
            meleeAttackInput = false;
        }
        else if (magicAttackInput)
        {
            MagicAttack magicAttack = new MagicAttack(character, character.magicalDamage);
            magicAttack.Execute();
            magicAttackInput = false;
        }
    }
}