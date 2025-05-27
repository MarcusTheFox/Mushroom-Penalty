using UnityEngine;

namespace Enemies.Data
{
    [CreateAssetMenu(fileName = "BossDataSO", menuName = "Scriptable Objects/BossDataSO")]
    public class BossDataSO : EnemyDataSO
    {
        public float speed;
    }
}
