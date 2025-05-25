using System;
using Combat.Interfaces;

namespace Player.CoreLogic.Handlers
{
    public class PlayerAttackInputHandler
    {
        private IAttack meleeAttack;
        private IAttack magicAttack;
    
        public event Action OnStartAttack;

        public PlayerAttackInputHandler(IAttack meleeAttack, IAttack magicAttack)
        {
            this.meleeAttack = meleeAttack;
            this.magicAttack = magicAttack;
        }

        public void OnMeleeAttack(bool value)
        {
            if (!value) return;
            meleeAttack.Start();
            OnStartAttack?.Invoke();
        }

        public void OnMagicAttack(bool value)
        {
            if (!value) return;
            magicAttack.Start();
            OnStartAttack?.Invoke();
        }
    }
}
