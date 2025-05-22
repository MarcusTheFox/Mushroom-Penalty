using Controller;
using UnityEngine;
using View;
using Object = UnityEngine.Object;

public class Player
{
    public PlayerInputController PIC;
    public AnimationEventListener AEL;
    public UnityEventListener UEL;
    public PlayerUI UI;
    public Transform playerTransform;
    public Animator Animator;
    
    public LayerMask targetLayer;
    public GameObject fireballPrefab;

    private PlayerCoreComponentsSetup coreSetup;
    private PlayerMovementSetup movementSetup;
    private PlayerAttackSetup attackSetup;
    private PlayerAnimationSetup animationSetup;
    public InteractableObjectEvents IOE;
    public Transform fireballSpawnPoint;

    public void Initialize()
    {
        coreSetup = new PlayerCoreComponentsSetup(PIC, IOE, 100);
        coreSetup.Initialize();

        movementSetup = new PlayerMovementSetup(PIC, UEL, playerTransform, 5f, 10f, 10f);
        movementSetup.Initialize();
        
        attackSetup = new PlayerAttackSetup(PIC, AEL, UEL, playerTransform, fireballPrefab, targetLayer, fireballSpawnPoint);
        attackSetup.Initialize();

        animationSetup = new PlayerAnimationSetup(Animator, PIC, coreSetup.Damageable);
        animationSetup.Initialize();
        
        UI.Initialize(coreSetup.Health, attackSetup.MagicCooldown);

        AEL.OnDead += DestroyPlayer;
        
        UEL.OnDestroyEvent.AddListener(OnDestroy);
    }

    private void OnDestroy()
    {
        UEL.OnDestroyEvent.RemoveListener(OnDestroy);
        
        coreSetup.Cleanup();
        movementSetup.Cleanup();
        attackSetup.Cleanup();
        animationSetup.Cleanup();
        UI.Cleanup();
    }

    private void DestroyPlayer()
    {
        AEL.OnDead -= DestroyPlayer;
        Object.Destroy(playerTransform.gameObject);
    }
}
