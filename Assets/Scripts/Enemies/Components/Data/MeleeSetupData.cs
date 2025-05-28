using Core.UnityHooks;
using UnityEngine;

namespace Enemies.Components.Data
{
    public struct MeleeSetupData
    {
        public AnimationEventListener AEL;
        public Transform EnemyTransform;
        public float Damage;
        public LayerMask TargetLayer;
        public float AttackRange;
        public float AttackAngle;
    }
}