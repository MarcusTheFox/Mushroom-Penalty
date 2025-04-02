using UnityEngine;

public abstract class BaseHealthUI : MonoBehaviour
{
    [SerializeField] protected Character character;
    protected HealthSystem healthSystem;

    protected virtual void Start()
    {
        if (character == null)
        {
            Debug.LogError("Character not assigned!", this);
            enabled = false;
            return;
        }

        healthSystem = character.HealthSystem;
        if (healthSystem == null)
        {
            Debug.LogError("HealthSystem not found on the assigned Character!", this);
            enabled = false;
            return;
        }
        healthSystem.OnHealthChanged += UpdateUI;
        InitializeUI();
    }

    protected virtual void OnDestroy()
    {
        if(healthSystem != null)
        {
          healthSystem.OnHealthChanged -= UpdateUI;
        }
    }

    protected abstract void UpdateUI();
    protected abstract void InitializeUI();
}