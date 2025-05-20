using Controller;
using UnityEngine;

public class Player
{
    public PlayerInputController PIC;
    public PlayerAnimationEventListener PAEL;
    public UnityEventListener UEL;
    public PlayerUI UI;
    public Transform playerTransform;
    public Animator Animator;

    private PlayerCoreComponentsSetup coreSetup;
    private PlayerMovementSetup movementSetup;
    private PlayerAttackSetup attackSetup;
    private PlayerAnimationSetup animationSetup;
    
    public void Initialize()
    {
        coreSetup = new PlayerCoreComponentsSetup();
        coreSetup.Initialize();

        movementSetup = new PlayerMovementSetup(PIC, UEL, playerTransform, 5f, 10f, 10f);
        movementSetup.Initialize();
        
        attackSetup = new PlayerAttackSetup(PIC, PAEL, UEL);
        attackSetup.Initialize();

        animationSetup = new PlayerAnimationSetup(Animator, PIC, coreSetup.Damageable);
        animationSetup.Initialize();
        
        UI.Initialize(coreSetup.Health, attackSetup.MagicCooldown);
        
        UEL.OnDestroyEvent.AddListener(OnDestroy);
    }

    private void OnDestroy()
    {
        UEL.OnDestroyEvent.RemoveListener(OnDestroy);
        
        movementSetup.Cleanup();
        attackSetup.Cleanup();
        animationSetup.Cleanup();
        UI.Cleanup();
    }
}
