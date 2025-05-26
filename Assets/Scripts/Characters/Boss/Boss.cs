using UnityEngine;
using System.Collections.Generic;

public class Boss : Character
{
    [Header("VFX")]
    public Renderer bossRenderer;
    public ParticleSystem elementalParticles;
    public ElementSettings[] elementSettings;

    [Header("Combat")]
    public float aggroRange = 10f;
    public Transform player;

    public ElementType CurrentElement { get; private set; }

    public BossStateMachine StateMachine { get; private set; }

    [HideInInspector] public int normalAttackCount = 0;
    public int strongAttackThreshold = 4;

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new BossStateMachine();
        StateMachine.AddState(StateType.Idle, new BossIdleState(this, StateMachine));
        StateMachine.AddState(StateType.Aggro, new BossAggroState(this, StateMachine));
        StateMachine.AddState(StateType.Attack, new BossAttackState(this, StateMachine));
        StateMachine.AddState(StateType.StrongAttack, new BossStrongAttackState(this, StateMachine));

        StateMachine.ChangeState(StateType.Idle);
    }

    private void Update()
    {
        StateMachine.CurrentState?.Update();
    }

    public void PerformNormalAttack()
    {
        SetRandomElement();
        ApplyElementColor();
        normalAttackCount++;
    }

    public void PerformStrongAttack()
    {
        SetRandomElement();
        PlayElementParticles();
    }

    private void SetRandomElement()
    {
        CurrentElement = (ElementType)Random.Range(0, System.Enum.GetValues(typeof(ElementType)).Length);
    }

    private void ApplyElementColor()
    {
        Color color = GetColorForElement(CurrentElement);
        if (bossRenderer != null)
        {
            bossRenderer.material.color = color;
        }
    }

    private void PlayElementParticles()
    {
        if (elementalParticles != null)
        {
            var main = elementalParticles.main;
            main.startColor = GetColorForElement(CurrentElement);
            elementalParticles.Play();
        }
    }

    private Color GetColorForElement(ElementType element)
    {
        foreach (var setting in elementSettings)
        {
            if (setting.element == element)
                return setting.color;
        }
        return Color.white;
    }

    private bool hasAggroTriggered = false;

    public override void TakeDamage(float damage, DamageType type)
    {
        base.TakeDamage(damage, type);
        Debug.Log("Босс получил урон");

        if (!hasAggroTriggered)
        {
            Debug.Log("Включаем агрессию");
            hasAggroTriggered = true;

            float distance = Vector3.Distance(transform.position, player.position);
            Debug.Log($"Дистанция до игрока: {distance}");

            if (distance <= aggroRange)
            {
                Debug.Log("Игрок близко — переходим в ATTTACK");
                StateMachine.ChangeState(StateType.Attack);
            }
            else
            {
                Debug.Log("Игрок далеко — переходим в AGGRO");
                StateMachine.ChangeState(StateType.Aggro);
            }
        }
    }


    private bool canMove = true;

    public bool CanMove => canMove;

    // Метод блокировки движения
    public void StopMoving()
    {
        canMove = false;
        // Если у тебя есть система перемещения, например NavMeshAgent, Rigidbody и т.п., останови движение здесь:
        // например:
        // navMeshAgent.isStopped = true;
    }

    // Метод разрешения движения
    public void StartMoving()
    {
        canMove = true;
        // navMeshAgent.isStopped = false;
    }

    public void OnAttackHit()
    {
        StopMoving();
        Debug.Log(normalAttackCount);
        PerformNormalAttack();
    }

    public void OnAttackEnd()
    {
        StartMoving();

        if (normalAttackCount >= strongAttackThreshold)
        {
            normalAttackCount = 0;
            StateMachine.ChangeState(StateType.StrongAttack);
        }
    }

    public void OnStrongAttackHit()
    {
        StopMoving();
        PerformStrongAttack();
        normalAttackCount = 0;
    }

    public void OnStrongAttackEnd()
    {

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= aggroRange)
        {
            StopMoving();
            Debug.Log("Игрок близко — переходим в ATTTACK");
            StateMachine.ChangeState(StateType.Attack);
        }
        else
        {
            StartMoving();
            Debug.Log("Игрок далеко — переходим в AGGRO");
            StateMachine.ChangeState(StateType.Aggro);
        }
    }

    protected override void Die()
    {
        animationController?.PlayBossDieAnimation();
    }

    public void DeadEvent()
    {
        Destroy(gameObject);
    }
}
