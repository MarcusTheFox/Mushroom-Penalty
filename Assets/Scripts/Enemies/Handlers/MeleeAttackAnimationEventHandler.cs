using System;
using Combat.Interfaces;

namespace Enemies.Handlers
{
    public class MeleeAttackAnimationEventHandler
    {
        private readonly IAttack attack;
        
        public event Action<IAttack> onApplyAttack;
        public event Action<IAttack> onStopAttack;

        public MeleeAttackAnimationEventHandler(IAttack attack)
        {
            this.attack = attack;
        }

        public void ApplyMeleeAttack()
        {
            attack.Apply();
            onApplyAttack?.Invoke(attack);
        }

        public void StopMeleeAttack()
        {
            attack.Stop();
            onStopAttack?.Invoke(attack);
        }
    }
}