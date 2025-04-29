using UnityEngine;
using UnityEngine.UI;

public class MagicCooldown : MonoBehaviour
{
    [SerializeField] private Slider cooldownSlider;
    [SerializeField] private MagicAttack magicAttack;
    [SerializeField] private Character player;

    private void Start()
    {
        if (magicAttack == null)
        {
            Debug.LogError("magicAttack not assigned to MagicAttackCooldownUI!", this);
            enabled = false;
            return;
        }

        if (cooldownSlider == null)
        {
            Debug.LogError("cooldownSlider not assigned to MagicAttackCooldownUI!", this);
            enabled = false;
            return;
        }

        cooldownSlider.minValue = 0;
        cooldownSlider.maxValue = 1;
        cooldownSlider.wholeNumbers = false;
        cooldownSlider.value = 1;
    }

    private void Update()
    {
        if (magicAttack != null)
        {
            UpdateCooldownUI();
        }
    }

    private void UpdateCooldownUI()
    {
        if (magicAttack.IsOnCooldown)
        {
            cooldownSlider.value = 1 - magicAttack.GetCooldownProgress();
        }
        else
        {
            cooldownSlider.value = 1;
        }
    }
}