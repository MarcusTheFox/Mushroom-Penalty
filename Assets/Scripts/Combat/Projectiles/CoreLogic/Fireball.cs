using Core.UnityHooks;
using UnityEngine;

namespace Combat.Projectiles.CoreLogic
{
    public class Fireball
    {
        private readonly UnityEventListener UEL;
        private readonly Transform transform;
        private readonly Rigidbody rigidbody;

        private float damage;
        private GameObject owner;

        public Fireball(UnityEventListener UEL, Transform transform, Rigidbody rigidbody)
        {
            this.UEL = UEL;
            this.transform = transform;
            this.rigidbody = rigidbody;
        }

        public void Initialize()
        {
            UEL.OnTriggerEnterEvent.AddListener(Hit);
            UEL.OnDestroyEvent.AddListener(OnDestroy);
        }

        public void AddSettings(GameObject owner, Vector3 direction, float speed, float damage)
        {
            this.owner = owner;
            rigidbody.linearVelocity = direction * speed;
            this.damage = damage;
        }

        private void OnDestroy()
        {
            UEL.OnTriggerEnterEvent.RemoveListener(Hit);
            UEL.OnDestroyEvent.RemoveListener(OnDestroy);
        }

        private void Hit(Collider collider)
        {
            if (!collider || collider.gameObject == owner) return;
            
            collider.GetComponent<InteractableObjectEvents>()?.AttemptDamage(damage);
            Object.Destroy(transform.gameObject);
        }
    }
}