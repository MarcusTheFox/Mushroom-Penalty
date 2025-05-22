using UnityEngine;
using UnityEngine.Serialization;
using View;

public class PlayerObjectInitializer : ObjectInitializer
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Transform fireballSpawnPoint;
    [SerializeField] private PlayerInputController PIC;
    private PlayerUI UI;
    private AnimationEventListener AEL;
    private InteractableObjectEvents IOE;
    private Animator animator;
    
    protected override void Initialize()
    {
        base.Initialize();
        AEL = GetComponent<AnimationEventListener>();
        IOE = GetComponent<InteractableObjectEvents>();
        animator = GetComponent<Animator>();
        
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Player player = new Player
        {
            PIC = PIC,
            AEL = AEL,
            UEL = UEL,
            IOE = IOE,
            UI = GetComponent<PlayerUI>(),
            playerTransform = transform,
            Animator = animator,
            fireballPrefab = fireballPrefab,
            targetLayer = targetLayer,
            fireballSpawnPoint = fireballSpawnPoint,
        };

        player.Initialize();
    }
}
