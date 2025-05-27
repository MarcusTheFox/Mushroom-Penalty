using System;
using Combat.Interfaces;

namespace Enemies.Handlers
{
    public class MagicAttackAnimationEventHandler
    {
        private readonly IAttack attack;
        
        public event Action<IAttack> onApplyAttack;
        public event Action<IAttack> onStopAttack;

        public MagicAttackAnimationEventHandler(IAttack attack)
        {
            this.attack = attack;
        }

        public void ApplyMagicAttack()
        {
            attack.Apply();
            onApplyAttack?.Invoke(attack);
        }

        public void StopMagicAttack()
        {
            attack.Stop();
            onStopAttack?.Invoke(attack);
        }
    }
}