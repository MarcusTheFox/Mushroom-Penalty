using Controller;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(Rigidbody))]
    public class FireballObjectInitializer : ObjectInitializer
    {
        private Fireball fireball;
        private const float Lifetime = 5f;

        protected override void Initialize()
        {
            base.Initialize();

            fireball = new Fireball()
            {
                UEL = UEL,
                transform = transform,
                Rigidbody = GetComponent<Rigidbody>(),
            };
            
            fireball.Initialize();
            Destroy(gameObject, Lifetime);
        }

        public void AddSettings(GameObject owner, Vector3 direction, float speed)
        {
            fireball.AddSettings(owner, direction, speed);
        }
    }
}