using UnityEngine;

public class Player
{
    public PlayerInputController PIC;
    public PlayerAnimationEventListener PAEL;
    public UnityEventListener UEL;
    public PlayerUI UI;
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
        
        movementInputHandler = new PlayerMovementInputHandler(movement, Transform, 5, 10, 10);
        attackInputHandler = new PlayerAttackInputHandler(meleeAttack, magicAttack);
        animationController = new PlayerAnimationController(Animator);
        attackAnimationEventHandler = new PlayerAttackAnimationEventHandler(meleeAttack, magicAttack);
        
        UI.Initialize(health, magicCooldown);
        
        AddMovementInputHandler();
        AddAttackInputHandler();
        AddAnimationInputHandler();
        AddAttackAnimationEventHandler();
        
        UEL.OnUpdateEvent.AddListener(magicCooldown.Update);
        UEL.OnDestroyEvent.AddListener(OnDestroy);
    }

    private void OnDestroy()
    {
        UEL.OnDestroyEvent.RemoveListener(OnDestroy);
        UEL.OnUpdateEvent.RemoveListener(magicCooldown.Update);
        
        UI.Cleanup();
        magicCooldown.ClearAndInvalidate();
        
        RemoveMovementInputHandler();
        RemoveAttackInputHandler();
        RemoveAnimationInputHandler();
        RemoveAttackAnimationEventHandler();
    }

    private void AddMovementInputHandler()
    {
        PIC.Move += movementInputHandler.OnMove;
        PIC.Run += movementInputHandler.OnRun;
        UEL.OnUpdateEvent.AddListener(movementInputHandler.OnUpdate);
    }

    private void RemoveMovementInputHandler()
    {
        PIC.Move -= movementInputHandler.OnMove;
        PIC.Run -= movementInputHandler.OnRun;
        UEL.OnUpdateEvent.RemoveListener(movementInputHandler.OnUpdate);
    }

    private void AddAttackInputHandler()
    {
        PIC.MeleeAttack += attackInputHandler.OnMeleeAttack;
        PIC.MagicAttack += attackInputHandler.OnMagicAttack;
    }

    private void RemoveAttackInputHandler()
    {
        PIC.MeleeAttack -= attackInputHandler.OnMeleeAttack;
        PIC.MagicAttack -= attackInputHandler.OnMagicAttack;
    }

    private void AddAnimationInputHandler()
    {
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
    }

    private void AddAttackAnimationEventHandler()
    {
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
    }
}
