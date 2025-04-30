using UnityEngine;

public class BossStateMachine : IStateMachine
{
    private readonly Boss _boss;

    public int AttackNumberCounter { get; private set; }
    public int DamageNumberCounter { get; private set; }

    public void ResetAttackNumberCounter()
    {
        AttackNumberCounter = 0;
    }
    public void ResetDamageNumberCounter()
    {
        DamageNumberCounter = 0;
    }
    public void IncreaseAttackNumberCounter()
    {
        AttackNumberCounter++;
    }
    public void IncreaseDamageNumberCounter()
    {
        DamageNumberCounter++;
    }

    public IState CurrentState { get; private set; }
    public IdleBossState IdleBossState { get; private set; }
    public AgressiveBossState AgressiveBossState { get; private set; }
    public AttackBossState AttackBossState { get; private set; }
    public StrongAttackBossState StrongAttackBossState { get; private set; }
    public ShieldBossState ShieldBossState { get; private set; }
    public HealingBossState HealingBossState { get; private set; }
    public KickAttackBossState KickAttackBossState { get; private set; }

    public BossStateMachine(Boss boss)
    {
        _boss = boss;

        IdleBossState = new IdleBossState(this, _boss);
        AgressiveBossState = new AgressiveBossState (this, _boss);
        AttackBossState = new AttackBossState (this, _boss);
        StrongAttackBossState = new StrongAttackBossState (this, _boss);
        ShieldBossState = new ShieldBossState (this, _boss);
        HealingBossState = new HealingBossState (this, _boss);
        KickAttackBossState = new KickAttackBossState (this, _boss);
    }

    public void Initialize(IState startingState)
    {
        ChangeState(startingState);
        Debug.Log($"Initialized BossStateMachine with state: {startingState.GetType().Name}", _boss);
    }

    public void Update()
    {
        CurrentState?.Update();
    }

    public void ChangeState(IState newState)
    {
        if (newState == null)
        {
            Debug.LogError("Cannot change state to null!", _boss);
            return;
        }

        if (CurrentState == newState)
        {
            Debug.LogWarning($"Trying to change to the same state: {newState.GetType().Name}", _boss);
            return;
        }

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }
}
