using UnityEngine;
using UnityEngine.UI;

public class HealthTextUI : BaseHealthUI
{
    [SerializeField] private Text healthText;
    [SerializeField] private string textFormat = "HP: {0}/{1}";

    protected override void UpdateUI()
    {
        healthText.text = string.Format(textFormat, healthSystem.CurrentHealth, healthSystem.MaxHealth);
    }

    protected override void InitializeUI()
    {
        UpdateUI();
    }
}