using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
        }
    }

    public void PlayMoveAnimation(bool isMoving, bool isRunning)
    {
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsRunning", isRunning);
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayDamageAnimation()
    {
        animator.SetTrigger("TakeDamage");
    }
    public void PlayDeathAnimation()
    {
        animator.SetTrigger("Die");
    }
}
