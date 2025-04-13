using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private string moveParamName = "Move";
    [SerializeField] private string runParamName = "Run";
    [SerializeField] private string meleeAttackParamName = "MeleeAttack";
    [SerializeField] private string magicAttackParamName = "MagicAttack";
    [SerializeField] private string damageParamName = "Damage";
    [SerializeField] private string deathParamName = "Death";
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
        animator.SetBool(moveParamName, isMoving);
        animator.SetBool(runParamName, isRunning);
    }

    public void PlayMeleeAttackAnimation()
    {
        animator.SetTrigger(meleeAttackParamName);
    }

    public void PlayMagicAttackAnimation()
    {
        animator.SetTrigger(magicAttackParamName);
    }

    public void PlayDamageAnimation()
    {
        animator.SetTrigger(damageParamName);
    }
    public void PlayDeathAnimation()
    {
        animator.SetTrigger(deathParamName);
    }
}
