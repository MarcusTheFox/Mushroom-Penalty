using UnityEngine;
using System.Collections.Generic;

public class Boss : Character
{
    [Header("Combat")]
    public float aggroRange = 10f;

    public ElementType CurrentElement { get; private set; }

    public BossStateMachine StateMachine { get; private set; }

    [HideInInspector] public int normalAttackCount = 0;
    public int strongAttackThreshold = 4;

    protected IMovable movement;

    [SerializeField] protected float damage = 10f;

    private BossAttack bossAttack;
    private BossStrongAttack bossStrongAttack;

    public Transform player;
    private PlayerCharacter playerScript;

    private bool hasAggroTriggered = false;
    private bool canMove = true;
    public bool CanMove => canMove;

    protected override void Awake()
    {
        base.Awake();

        movement = GetComponent<IMovable>();

        bossAttack = GetComponent<BossAttack>();
        bossStrongAttack = GetComponent<BossStrongAttack>();

        if (bossAttack == null || bossStrongAttack == null)
        {
            Debug.LogError("BossAttack или BossStrongAttack не найдены на объекте босса!");
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerScript = playerObj.GetComponent<PlayerCharacter>();
            if (playerScript == null)
            {
                Debug.LogError("Компонент PlayerCharacter не найден у объекта с тегом Player!");
            }
        }
        else
        {
            Debug.LogError("Игрок с тегом 'Player' не найден!");
        }

        StateMachine = new BossStateMachine();
        StateMachine.AddState(StateType.Idle, new BossIdleState(this, StateMachine));
        StateMachine.AddState(StateType.Aggro, new BossAggroState(this, StateMachine));
        StateMachine.AddState(StateType.Attack, new BossAttackState(this, StateMachine));
        StateMachine.AddState(StateType.StrongAttack, new BossStrongAttackState(this, StateMachine));

        StateMachine.ChangeState(StateType.Idle);
    }

    private void Update()
    {
        Debug.Log($"IsPlayerDead = {IsPlayerDead()}");

        if (IsPlayerDead())
        {
            StateMachine.ChangeState(StateType.Idle);
            return;
        }

        StateMachine.CurrentState?.Update();
    }

    public void InitializePlayer(Transform playerTransform)
    {
        player = playerTransform;

        playerScript = player.GetComponent<PlayerCharacter>();
        if (playerScript == null)
        {
            Debug.LogError("Компонент PlayerCharacter не найден у объекта игрока!");
        }

        bossAttack = GetComponent<BossAttack>();
        bossStrongAttack = GetComponent<BossStrongAttack>();

        if (bossAttack != null)
            bossAttack.SetTarget(player);

        if (bossStrongAttack != null)
            bossStrongAttack.SetTarget(player);
    }


    public override void TakeDamage(float damage, DamageType type)
    {
        base.TakeDamage(damage, type);
        Debug.Log("Босс получил урон");

        if (IsPlayerDead()) return;

        if (!hasAggroTriggered)
        {
            hasAggroTriggered = true;

            float distance = Vector3.Distance(transform.position, player.position);
            Debug.Log($"Дистанция до игрока: {distance}");

            if (distance <= aggroRange)
            {
                Debug.Log("Игрок близко — переходим в ATTACK");
                StateMachine.ChangeState(StateType.Attack);
            }
            else
            {
                Debug.Log("Игрок далеко — переходим в AGGRO");
                StateMachine.ChangeState(StateType.Aggro);
            }
        }
    }

    public void PerformNormalAttack()
    {
        if (IsPlayerDead()) return;

        bossAttack?.PerformAttack();
        normalAttackCount++;
    }

    public void PerformStrongAttack()
    {
        if (IsPlayerDead()) return;

        bossStrongAttack?.PerformAttack();
    }

    public void OnAttackHit()
    {
        if (IsPlayerDead()) return;

        StopMoving();
        PerformNormalAttack();

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance >= aggroRange)
        {
            StateMachine.ChangeState(StateType.Aggro);
        }
    }

    public void OnAttackEnd()
    {
        if (IsPlayerDead())
        {
            StateMachine.ChangeState(StateType.Idle);
            return;
        }

        StartMoving();

        if (normalAttackCount >= strongAttackThreshold)
        {
            normalAttackCount = 0;
            StateMachine.ChangeState(StateType.StrongAttack);
        }
    }

    public void OnStrongAttackHit()
    {
        if (IsPlayerDead()) return;

        StopMoving();
        PerformStrongAttack();
        normalAttackCount = 0;
    }

    public void OnStrongAttackEnd()
    {
        if (IsPlayerDead())
        {
            StateMachine.ChangeState(StateType.Idle);
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= aggroRange)
        {
            StopMoving();
            StateMachine.ChangeState(StateType.Attack);
        }
        else
        {
            StartMoving();
            StateMachine.ChangeState(StateType.Aggro);
        }
    }

    public void MoveToPlayer()
    {
        if (!canMove || player == null || IsPlayerDead()) return;

        LookToPlayer();
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        movement?.Move(directionToPlayer);
    }

    public void LookToPlayer()
    {
        if (player == null || IsPlayerDead()) return;

        Vector3 lookTarget = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookTarget);
    }

    protected override void Die()
    {
        animationController?.PlayBossDieAnimation();
    }

    public void DeadEvent()
    {
        Destroy(gameObject);
    }

    public void StopMoving() => canMove = false;
    public void StartMoving() => canMove = true;

    public bool IsPlayerDead()
    {
        return playerScript == null || playerScript.HealthSystem == null || playerScript.HealthSystem.IsDead;
    }

}
