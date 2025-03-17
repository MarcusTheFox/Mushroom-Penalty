using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    public RectTransform healthBarFill;
    private IDamageable damageableTarget;

    private void Start()
    {
        damageableTarget = GetComponentInParent<IDamageable>();
        if (damageableTarget == null)
        {
            Debug.LogError("No IDamageable found in parents of UIHealthBar!");
            enabled = false;
            return;
        }
        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (damageableTarget != null)
        {
            float healthPercentage = damageableTarget.GetCurrentHealth() / damageableTarget.GetMaxHealth();
            healthBarFill.localScale = new Vector3(healthPercentage, 1, 1);

            if (damageableTarget is MonoBehaviour targetBehaviour)
            {
                transform.LookAt(Camera.main.transform);
            }

        }
    }
}