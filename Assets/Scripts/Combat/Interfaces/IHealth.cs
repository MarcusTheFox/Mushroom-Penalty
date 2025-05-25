using System;

namespace Combat.Interfaces
{
    public interface IHealth
    {
        public float Health { get; }
        public float MaxHealth { get; }
        public event Action<float> OnChange;
        public event Action<float> OnIncrease;
        public event Action<float> OnDecrease;
    
        public void Increase(float amount);
        public void Decrease(float amount);
    }
}
