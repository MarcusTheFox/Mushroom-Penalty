namespace Controller
{
    public class PlayerCoreComponentsSetup
    {
        public IHealth Health { get; private set; }
        public IDamageable Damageable { get; private set; }
        
        public void Initialize()
        {
            Health = new HealthComponent(100);
            Damageable = new DamageableComponent(Health);
        }
    }
}