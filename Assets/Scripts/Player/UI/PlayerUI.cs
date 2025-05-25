using Combat.Interfaces;
using Core.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Player.UI
{
    public class PlayerUI : MonoBehaviour, ICleanupable
    {
        [SerializeField] private Text healthText;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider manaSlider;
    
        private IHealth health;
        private ICooldown cooldown;
    
        private float healthBarChangeSpeed = 0.5f;

        public void Initialize(IHealth healthComponent, ICooldown magicCooldown)
        {
            health = healthComponent;
            cooldown = magicCooldown;

            health.OnChange += UpdateHealth;
            cooldown.OnUpdate += UpdateMana;
        }

        public void Cleanup()
        {
            health.OnChange -= UpdateHealth;
            cooldown.OnUpdate -= UpdateMana;
        }
    
        private void UpdateHealth(float value)
        {
            healthText.text = $"HP: {health.Health:0} | {health.MaxHealth:0}";
            healthSlider.DOValue(Mathf.Clamp01(health.Health / health.MaxHealth), healthBarChangeSpeed);
        }

        private void UpdateMana()
        {
            manaSlider.value = cooldown.ProgressNormalized;
        }
    }
}
