using System;
using Combat.Interfaces;

namespace Player.CoreLogic.Handlers
{
    public class PlayerAttackAnimationEventHandler
    {
        private IAttack meleeAttack;
        private IAttack magicAttack;
    
        public event Action<IAttack> onApplyAttack;
        public event Action<IAttack> onStopAttack;

        public PlayerAttackAnimationEventHandler(IAttack meleeAttack, IAttack magicAttack)
        {
            this.meleeAttack = meleeAttack;
            this.magicAttack = magicAttack;
        }

        public void ApplyMeleeAttack()
        {
            meleeAttack.Apply();
            onApplyAttack?.Invoke(meleeAttack);
        }

        public void ApplyMagicAttack()
        {
            magicAttack.Apply();
            onApplyAttack?.Invoke(magicAttack);
        }

        public void StopMeleeAttack()
        {
            meleeAttack.Stop();
            onStopAttack?.Invoke(meleeAttack);
        }

        public void StopMagicAttack()
        {
            magicAttack.Stop();
            onStopAttack?.Invoke(magicAttack);
        }
    }
}
