using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationController
{
    private Animator animator;

    public PlayerAnimationController(Animator animator)
    {
        this.animator = animator;
    }

    public void OnMove(Vector2 value)
    {
        animator.SetBool("Walk", value != Vector2.zero);
    }

    public void OnRun(bool value)
    {
        animator.SetBool("Run", value);
    }

    public void OnMeleeAttack(bool value)
    {
        if (value) animator.SetTrigger("PhysicalAttack");
    }

    public void OnMagicAttack(bool value)
    {
        if (value) animator.SetTrigger("MagicAttack");
    }

    public void OnDie()
    {
        animator.SetTrigger("NoHP");
    }
}
