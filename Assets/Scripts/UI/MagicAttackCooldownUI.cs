using UnityEngine;
using UnityEngine.UI;

public class MagicAttackCooldownUI : MonoBehaviour
{
    [SerializeField] private Image cooldownImageFiller;
    [SerializeField] private MagicAttack magicAttack;
    [SerializeField] private Character player;
    
    private void Start()
    {
        if (magicAttack == null)
        {
            Debug.LogError("magicAttack not assigned to MagicAttackCooldownUI!");
            enabled = false;
            return;
        }
        
        if (cooldownImageFiller == null)
        {
            Debug.LogError("cooldownImage not assigned to MagicAttackCooldownUI!");
            enabled = false;
            return;
        }
        cooldownImageFiller.fillAmount = 0;
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
            cooldownImageFiller.gameObject.SetActive(true);
            cooldownImageFiller.fillAmount = magicAttack.GetCooldownProgress();
        }
        else
        {
            cooldownImageFiller.gameObject.SetActive(false);
        }
    }
}


