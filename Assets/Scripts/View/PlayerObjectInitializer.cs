using UnityEngine;

public class PlayerObjectInitializer : ObjectInitializer
{
    [SerializeField] private PlayerInputController PIC;
    private PlayerUI UI;
    private PlayerAnimationEventListener PAEL;
    
    protected override void Initialize()
    {
        base.Initialize();
        PAEL = GetComponent<PlayerAnimationEventListener>();
        
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Player player = new Player();
        
        player.PIC = PIC;
        player.PAEL = PAEL;
        player.UEL = UEL;
        player.UI = GetComponent<PlayerUI>();
        player.Transform = transform;
        player.Animator = animator;
        
        player.Initialize();
    }
}
