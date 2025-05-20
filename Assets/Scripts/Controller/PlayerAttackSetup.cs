using Model;

namespace Controller
{
    public class PlayerAttackSetup : ICleanupable
    {
        public ICooldown MagicCooldown { get; private set; }
        public IAttack MeleeAttack { get; private set; }
        public IAttack MagicAttack { get; private set; }

        private PlayerInputController PIC;
        private PlayerAnimationEventListener PAEL;
        private UnityEventListener UEL;
        
        private PlayerAttackInputHandler attackInputHandler;
        private PlayerAttackAnimationEventHandler attackAnimationEventHandler;

        public PlayerAttackSetup(PlayerInputController PIC, PlayerAnimationEventListener PAEL, UnityEventListener UEL)
        {
            this.PIC = PIC;
            this.PAEL = PAEL;
            this.UEL = UEL;
        }

        public void Initialize()
        {
            MagicCooldown = new Cooldown(5);
            MeleeAttack = new MeleeAttack(30);
            MagicAttack = new MagicAttack(50, MagicCooldown);
            
            attackInputHandler = new PlayerAttackInputHandler(MeleeAttack, MagicAttack);
            attackAnimationEventHandler = new PlayerAttackAnimationEventHandler(MeleeAttack, MagicAttack);

            UEL.OnUpdateEvent.AddListener(MagicCooldown.Update);
            
            AddAttackInputHandler();
            AddAttackAnimationEventHandler();
        }

        public void Cleanup()
        {
            UEL.OnUpdateEvent.RemoveListener(MagicCooldown.Update);
            MagicCooldown.ClearAndInvalidate();
            
            RemoveAttackInputHandler();
            RemoveAttackAnimationEventHandler();
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
}