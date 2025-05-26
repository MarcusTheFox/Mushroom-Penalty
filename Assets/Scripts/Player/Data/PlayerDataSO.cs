using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Scriptable Objects/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    public float health = 100f;
    public float speed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 10f;
    public LayerMask targetLayer;
    [Space]
    public float meleeDamage = 10f;
    public float meleeRange = 10f;
    public float meleeAngle = 90f;
    [Space]
    public float magicDamage = 30f;
    public float magicCooldown = 10f;
    public GameObject magicProjectilePrefab;
}
