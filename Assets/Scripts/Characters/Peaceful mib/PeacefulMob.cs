using UnityEngine;

[RequireComponent(typeof(IMovable))] // тут лучше заменить на конкретный класс, если есть
public class PeacefulMob : Character
{
    [SerializeField] public float fleeDistance = 10f;
    [SerializeField] private float fleeSpeed = 1f;

    [SerializeField] private Transform player; // теперь можно задать в инспекторе

    private IMovable movement;
    public PeacefulStateMachine stateMachine;



    protected override void Awake()
    {
        base.Awake();
        Debug.Log("PeacefulMob Awake");

        movement = GetComponent<IMovable>();

        stateMachine = new PeacefulStateMachine();
        stateMachine.AddState(StateType.Idle, new PeacefulIdleState(this));
        stateMachine.AddState(StateType.Flee, new PeacefulFleeState(this));

    }

    private void Start()
    {
        Debug.Log("PeacefulMob Start");

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                Debug.LogWarning("Player с тегом 'Player' не найден!");
        }

        stateMachine.ChangeState(StateType.Idle);
    }

    private void Update()
    {
        Debug.Log("PeacefulMob Update");

        if (stateMachine.CurrentState == null)
        {
            Debug.LogWarning("CurrentState is NULL!");
        }
        else
        {
            Debug.Log($"CurrentState: {stateMachine.CurrentState.GetType().Name}");
            stateMachine.CurrentState.Update();
        }
    }


    public override void TakeDamage(float damage, DamageType type)
    {
        base.TakeDamage(damage, type);
        Debug.Log("PeacefulMob получил урон");

        if (stateMachine != null)
        {
            stateMachine.ChangeState(StateType.Flee);
        }
    }

    public void LookAwayFromPlayer()
    {
        if (player == null) return;

        Vector3 lookDirection = transform.position - player.position;
        lookDirection.y = 0;

        if (lookDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            // Например, если модель смотрит вперёд по локальному X, повернём на 90 градусов вокруг Y:
            targetRotation *= Quaternion.Euler(0, 180f, 0);

            transform.rotation = targetRotation;
        }
    }



    public void FleeFromPlayer()
    {
        Debug.Log("FleeFromPlayer вызван");
        if (player == null || movement == null) return;

        LookAwayFromPlayer();

        Vector3 direction = (transform.position - player.position).normalized;
        movement.Move(direction * fleeSpeed);
    }

    public float GetDistanceToPlayer()
    {
        if (player == null) return float.MaxValue;
        return Vector3.Distance(transform.position, player.position);
    }

    protected override void Die()
    {
        animationController?.PlayBossDieAnimation();
        // Уничтожение объекта лучше сделать после анимации через событие
    }

    public void DeadEvent()
    {
        Destroy(gameObject);
    }
}
