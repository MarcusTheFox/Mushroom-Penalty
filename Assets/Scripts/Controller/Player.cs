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
    private PlayerAnimationController animationController;
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
        
        UEL.OnDestroyEvent.AddListener(OnDestroy);
    }

    private void OnDestroy()
    {
        magicCooldown.ClearAndInvalidate();
        
        RemoveMovementInputHandler();
        RemoveAttackInputHandler();
        RemoveAnimationInputHandler();
        RemoveAttackAnimationEventHandler();
    }

    private void AddMovementInputHandler()
    {
        movementInputHandler = new PlayerMovementInputHandler(movement, Transform, 5, 10, 10);
        
        PIC.Move += movementInputHandler.OnMove;
        PIC.Run += movementInputHandler.OnRun;
        UEL.OnUpdateEvent.AddListener(movementInputHandler.OnUpdate);
    }

    private void RemoveMovementInputHandler()
    {
        PIC.Move -= movementInputHandler.OnMove;
        PIC.Run -= movementInputHandler.OnRun;
        UEL.OnUpdateEvent.RemoveListener(movementInputHandler.OnUpdate);
        
        movementInputHandler = null;
    }

    private void AddAttackInputHandler()
    {
        attackInputHandler = new PlayerAttackInputHandler(meleeAttack, magicAttack);

        PIC.MeleeAttack += attackInputHandler.OnMeleeAttack;
        PIC.MagicAttack += attackInputHandler.OnMagicAttack;
    }

    private void RemoveAttackInputHandler()
    {
        PIC.MeleeAttack -= attackInputHandler.OnMeleeAttack;
        PIC.MagicAttack -= attackInputHandler.OnMagicAttack;
        
        attackInputHandler = null;
    }

    private void AddAnimationInputHandler()
    {
        animationController = new PlayerAnimationController(Animator);

        PIC.Move += animationController.OnMove;
        PIC.Run += animationController.OnRun;
        PIC.MeleeAttack += animationController.OnMeleeAttack;
        PIC.MagicAttack += animationController.OnMagicAttack;
        damageable.OnDeath += animationController.OnDie;
    }

    private void RemoveAnimationInputHandler()
    {
        PIC.Move -= animationController.OnMove;
        PIC.Run -= animationController.OnRun;
        PIC.MeleeAttack -= animationController.OnMeleeAttack;
        PIC.MagicAttack -= animationController.OnMagicAttack;
        damageable.OnDeath -= animationController.OnDie;
        
        animationController = null;
    }

    private void AddAttackAnimationEventHandler()
    {
        attackAnimationEventHandler = new PlayerAttackAnimationEventHandler(meleeAttack, magicAttack);

        PAEL.OnApplyMeleeAttack += attackAnimationEventHandler.ApplyMeleeAttack;
        PAEL.OnApplyMagicAttack += attackAnimationEventHandler.ApplyMagicAttack;
        PAEL.OnStopMeleeAttack += attackAnimationEventHandler.StopMeleeAttack;
        PAEL.OnStopMagicAttack += attackAnimationEventHandler.StopMagicAttack;
    }

    private void RemoveAttackAnimationEventHandler()
    {
        PAEL.OnApplyMeleeAttack -= attackAnimationEventHandler.ApplyMeleeAttack;
        PAEL.OnApplyMagicAttack -= attackAnimationEventHandler.ApplyMagicAttack;
        PAEL.OnStopMeleeAttack -= attackAnimationEventHandler.StopMeleeAttack;
        PAEL.OnStopMagicAttack -= attackAnimationEventHandler.StopMagicAttack;
        
        attackAnimationEventHandler = null;
    }
}
