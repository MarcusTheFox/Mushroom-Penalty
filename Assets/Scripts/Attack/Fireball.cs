using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    private IDamageable shooter;
    private float damage;

    public void Initialize(IDamageable shooter, Vector3 direction, float damage)
    {
        this.shooter = shooter;
        this.damage = damage;
        GetComponent<Rigidbody>().linearVelocity = direction.normalized * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null && damageable != shooter)
        {
            damageable.TakeDamage(damage, DamageType.Magical);
            Destroy(gameObject);
        }
        
        if(other.gameObject != ((MonoBehaviour)shooter).gameObject)
        {
            Destroy(gameObject);
        }
    }
}