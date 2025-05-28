using Core.UnityHooks;
using UnityEngine;

namespace Enemies.Components.Data
{
    public struct MagicSetupData
    {
        public UnityEventListener UEL;
        public AnimationEventListener AEL;
        public Transform EnemyTransform;
        public Transform FireballSpawnPoint;
        public float Damage;
        public LayerMask TargetLayer;
        public float Cooldown;
        public GameObject ProjectilePrefab;
    }
}