using Controller;
using View;

public class EnemyMeleeObjectInitializer : ObjectInitializer
{
    private EnemyUI UI;
    private AnimationEventListener AEL;
    
    protected override void Initialize()
    {
        base.Initialize();
        AEL = GetComponent<AnimationEventListener>();
        
        CreateEnemy();
    }

    private void CreateEnemy()
    {
        EnemyMelee enemyMelee = new EnemyMelee();
        
        enemyMelee.AEL = AEL;
        enemyMelee.UEL = UEL;
        enemyMelee.UI = GetComponent<EnemyUI>();
        enemyMelee.enemyTransform = transform;
        enemyMelee.Animator = animator;
        
        enemyMelee.Initialize();
    }
}
