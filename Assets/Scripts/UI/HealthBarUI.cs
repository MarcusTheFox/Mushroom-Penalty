using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : BaseHealthUI
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private float barAnimationDuration = 0.3f;

    protected override void InitializeUI()
    {
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = healthSystem.MaxHealth;
        healthBarSlider.value = healthSystem.CurrentHealth;
        healthBarSlider.wholeNumbers = false;
    }

    protected override void UpdateUI()
    {
        healthBarSlider.DOValue(character.GetCurrentHealth(), barAnimationDuration);
    }
}