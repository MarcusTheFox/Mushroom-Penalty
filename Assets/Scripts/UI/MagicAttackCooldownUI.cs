using UnityEngine;
using UnityEngine.UI;

public class MagicAttackCooldownUI : MonoBehaviour
{
    [SerializeField] private Image cooldownImageFiller;
    private MagicAttacker magicAttacker;
    
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            magicAttacker = player.GetComponent<MagicAttacker>();
        }
        if (magicAttacker == null)
        {
            Debug.LogError("MagicAttacker not found!");
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
        if (magicAttacker != null)
        {
            UpdateCooldownUI();
        }
    }

    private void UpdateCooldownUI()
    {
        if (magicAttacker.IsOnCooldown)
        {
            cooldownImageFiller.gameObject.SetActive(true);
            cooldownImageFiller.fillAmount = magicAttacker.GetCooldownProgress();
        }
        else
        {
            cooldownImageFiller.gameObject.SetActive(false);
        }
    }
}