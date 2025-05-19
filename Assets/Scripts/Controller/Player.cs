using UnityEngine;
using UnityEngine.InputSystem;

public class Player
{
    public PlayerInputController PIC;
    public PlayerAnimationEventListener PAEL;
    public UnityEventListener UEL;
    public UI UI;
    public Transform Transform;
    public Animator Animator;
    
    private PlayerMovementInputHandler movementInputHandler;
    private PlayerAttackInputHandler attackInputHandler;
    private PlayerAnimationInputHandler animationInputHandler;
    private PlayerAttackAnimationEventHandler attackAnimationEventHandler;

    private ICooldown magicCooldown;
    private IAttack meleeAttack;
    private IAttack magicAttack;
    private IHealth health;
    private IDamageable damageable;
    private IMovement movement;

    private Rigidbody rigidbody;
    private Transform cameraTransform;
    
    public void Initialize()
    {
        cameraTransform = Camera.main?.transform;
        
        magicCooldown = new Cooldown(5);
        meleeAttack = new MeleeAttack(30);
        magicAttack = new MagicAttack(50, magicCooldown);
        health = new HealthComponent(100);
        damageable = new DamageableComponent(health);
        movement = new PlayerMovement(cameraTransform);
        
        
        AddMovementInputHandler();
        AddAttackInputHandler();
        AddAnimationInputHandler();
        AddAttackAnimationEventHandler();
    }

    private void AddMovementInputHandler()
    {
        movementInputHandler = new PlayerMovementInputHandler(movement, Transform, 5, 10, 10);
        
        PIC.Move += movementInputHandler.OnMove;
        PIC.Run += movementInputHandler.OnRun;
        UEL.OnUpdateEvent.AddListener(movementInputHandler.OnUpdate);
    }

    private void AddAttackInputHandler()
    {
        attackInputHandler = new PlayerAttackInputHandler(meleeAttack, magicAttack);

        PIC.MeleeAttack += attackInputHandler.OnMeleeAttack;
        PIC.MagicAttack += attackInputHandler.OnMagicAttack;
    }

    private void AddAnimationInputHandler()
    {
        animationInputHandler = new PlayerAnimationInputHandler(Animator);

        PIC.Move += animationInputHandler.OnMove;
        PIC.Run += animationInputHandler.OnRun;
        PIC.MeleeAttack += animationInputHandler.OnMeleeAttack;
        PIC.MagicAttack += animationInputHandler.OnMagicAttack;
    }

    private void AddAttackAnimationEventHandler()
    {
        attackAnimationEventHandler = new PlayerAttackAnimationEventHandler(meleeAttack, magicAttack);
        
        
    }
}
