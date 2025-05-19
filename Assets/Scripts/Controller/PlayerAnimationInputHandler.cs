using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationInputHandler
{
    private Animator animator;

    public PlayerAnimationInputHandler(Animator animator)
    {
        this.animator = animator;
    }

    public void OnMove(InputValue inputValue)
    {
        animator.SetBool("Walk", inputValue.Get<Vector2>() != Vector2.zero);
    }

    public void OnRun(InputValue inputValue)
    {
        animator.SetBool("Run", inputValue.isPressed);
    }

    public void OnMeleeAttack(InputValue inputValue)
    {
        if (inputValue.isPressed) animator.SetTrigger("PhysicalAttack");
    }

    public void OnMagicAttack(InputValue inputValue)
    {
        if (inputValue.isPressed) animator.SetTrigger("MagicAttack");
    }

    public void OnDie()
    {
        animator.SetTrigger("NoHP");
    }
}
