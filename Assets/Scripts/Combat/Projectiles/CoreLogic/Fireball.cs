using Core.UnityHooks;
using UnityEngine;

namespace Combat.Projectiles.CoreLogic
{
    public class Fireball
    {
        public UnityEventListener UEL;
        public Transform transform;
        public Rigidbody Rigidbody;

        private float damage = 50f;
        private GameObject owner;
        private Vector3 direction;

        public void Initialize()
        {
            UEL.OnTriggerEnterEvent.AddListener(Hit);
            
            UEL.OnDestroyEvent.AddListener(OnDestroy);
        }

        public void AddSettings(GameObject owner, Vector3 direction, float speed)
        {
            this.owner = owner;
            this.direction = direction;
            Rigidbody.linearVelocity = direction * speed;
        }

        private void OnDestroy()
        {
            UEL.OnTriggerEnterEvent.RemoveListener(Hit);
            UEL.OnDestroyEvent.RemoveListener(OnDestroy);
        }

        private void Hit(Collider collider)
        {
            if (collider.gameObject == owner) return;
            
            collider.GetComponent<InteractableObjectEvents>()?.AttemptDamage(damage);
            Object.Destroy(transform.gameObject);
        }
    }
}