using DG.Tweening;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class EnemyUI : MonoBehaviour, ICleanupable
    {
        [SerializeField] private Slider healthSlider;
    
        private IHealth health;
    
        private float healthBarChangeSpeed = 0.5f;

        public void Initialize(IHealth healthComponent)
        {
            health = healthComponent;

            health.OnChange += UpdateHealth;
        }

        public void Cleanup()
        {
            health.OnChange -= UpdateHealth;
        }
    
        private void UpdateHealth(float value)
        {
            healthSlider.DOValue(Mathf.Clamp01(health.Health / health.MaxHealth), healthBarChangeSpeed);
        }
    }
}
