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
    [SerializeField] private string attackParamName = "Attack";
    [SerializeField] private string strongAttackParamName = "StrongAttack";
    [SerializeField] private string dieParamName = "Die";
    [SerializeField] private string aggroParamName = "Aggro";
    [SerializeField] private string fleeParamName = "Flee";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
        }
    }

    public void PlayFleeAnimation(bool isFlee)
    {
        animator.SetBool(fleeParamName, isFlee);
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

    public void PlayBossAttackAnimation(bool IsAttack)
    {
        animator.SetBool(attackParamName, IsAttack); 
    }

    public void PlayBossStrongAttackAnimation()
    {
        animator.SetTrigger(strongAttackParamName); 
    }

    public void DontPlayBossStrongAttackAnimation()
    {
        animator.ResetTrigger(strongAttackParamName);
    }

    public void PlayBossAggroAnimation(bool isAggro)
    {
        animator.SetBool(aggroParamName, isAggro); 
    }

    public void PlayBossDieAnimation()
    {
        animator.SetTrigger(dieParamName); 
    }

}
