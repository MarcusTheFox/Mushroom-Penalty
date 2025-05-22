using Controller;
using UnityEngine;
using View;

public class EnemyMeleeObjectInitializer : ObjectInitializer
{
    private EnemyUI UI;
    private AnimationEventListener AEL;
    private InteractableObjectEvents IOE;
    private Animator animator;

    protected override void Initialize()
    {
        base.Initialize();
        AEL = GetComponent<AnimationEventListener>();
        IOE = GetComponent<InteractableObjectEvents>();
        animator = GetComponent<Animator>();
        
        CreateEnemy();
    }

    private void CreateEnemy()
    {
        EnemyMelee enemyMelee = new EnemyMelee
        {
            AEL = AEL,
            UEL = UEL,
            IOE = IOE,
            UI = GetComponent<EnemyUI>(),
            enemyTransform = transform,
            Animator = animator
        };

        enemyMelee.Initialize();
    }
}
