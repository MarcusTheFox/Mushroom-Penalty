using UnityEngine;

public class PlayerObjectInitializer : ObjectInitializer
{
    [SerializeField] private PlayerInputController PIC;
    private PlayerUI UI;
    private AnimationEventListener AEL;
    
    protected override void Initialize()
    {
        base.Initialize();
        AEL = GetComponent<AnimationEventListener>();
        
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Player player = new Player();
        
        player.PIC = PIC;
        player.AEL = AEL;
        player.UEL = UEL;
        player.UI = GetComponent<PlayerUI>();
        player.playerTransform = transform;
        player.Animator = animator;
        
        player.Initialize();
    }
}
