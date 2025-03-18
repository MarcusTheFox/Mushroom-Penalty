using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Character character;
    private HealthSystem healthSystem;

    private void Start()
    {
        if (character == null)
        {
            Debug.LogError("Character is not set in the Inspector for UIHealthBar!", this);
            enabled = false;
            return;
        }

        healthSystem = character.HealthSystem;
        if (healthSystem == null)
        {
            Debug.LogError("HealthSystem not found on the assigned Damageable Target!", this);
            enabled = false;
            return;
        }

        healthSystem.OnHealthChanged += UpdateHealthBar;
        InitializeHealthBar();
    }
    
    private void OnDestroy()
    {
        if(healthSystem != null)
        {
            healthSystem.OnHealthChanged -= UpdateHealthBar;
        }
    }

    private void InitializeHealthBar()
    {
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = healthSystem.MaxHealth;
        healthBarSlider.value = healthSystem.CurrentHealth;
        healthBarSlider.wholeNumbers = false;
    }

    private void UpdateHealthBar()
    {
        healthBarSlider.value = character.GetCurrentHealth();
    }
}