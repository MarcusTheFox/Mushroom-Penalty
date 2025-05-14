using UnityEngine;

public sealed class FireballAttack : MagicAttack
{
    [SerializeField] private GameObject[] fireballPrefabs; // массив фаерболов
    [SerializeField] private float[] fireballDamages;      // соответствующий урон
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireballSpeed = 10f;

    protected override void Awake()
    {
        base.Awake();
        if (fireballPrefabs.Length != fireballDamages.Length)
        {
            Debug.LogError("Количество фаерболов и урона должно совпадать!");
            enabled = false;
        }
        if (fireballPrefabs.Length == 0 || firePoint == null)
        {
            Debug.LogError("FireballAttack не настроен корректно!");
            enabled = false;
        }
    }

    public override void MagicAttackEvent()
    {
        int index = Random.Range(0, fireballPrefabs.Length);
        GameObject prefab = fireballPrefabs[index];
        float selectedDamage = fireballDamages[index];

        GameObject fireball = Instantiate(prefab, firePoint.position, firePoint.rotation);
        Fireball fireballComponent = fireball.GetComponent<Fireball>();

        if (fireballComponent != null)
        {
            fireballComponent.Initialize(owner, firePoint.forward, selectedDamage);
        }
        else
        {
            Debug.LogError("Fireball Prefab does not have Fireball Component!", prefab);
        }
    }

    private void OnDrawGizmos()
    {
        if (firePoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(firePoint.position, firePoint.forward * 10);
        }
    }
}



//public sealed class FireballAttack : MagicAttack
//{
//    [SerializeField] private GameObject fireballPrefab;
//    [SerializeField] private Transform firePoint;
//    [SerializeField] private float fireballSpeed = 10f;

//     protected override void Awake()
//    {
//        base.Awake();
//        if (fireballPrefab == null)
//        {
//            Debug.LogError("Fireball prefab not assigned to FireballAttack!");
//            enabled = false;
//            return;
//        }
//        if (firePoint == null)
//        {
//            Debug.LogError("FirePoint not assigned to FireballAttack!");
//            enabled = false;
//            return;
//        }
//    }

//    public override void MagicAttackEvent()
//    {
//        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
//        Fireball fireballComponent = fireball.GetComponent<Fireball>();

//        if (fireballComponent != null)
//        {
//            fireballComponent.Initialize(owner, firePoint.forward, damage);
//        }
//        else
//        {
//            Debug.LogError("Fireball Prefab does not have Fireball Component!", fireballPrefab);
//        }
//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.yellow;
//        Gizmos.DrawRay(firePoint.position, firePoint.forward * 10);
//    }
//}
