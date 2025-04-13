using UnityEngine;

public class CooldownSystem
{
    public float CooldownDuration { get; private set; }
    public float CooldownTimer { get; private set; }

    public bool IsOnCooldown => CooldownTimer > 0;

    public CooldownSystem(float cooldownDuration)
    {
        CooldownDuration = cooldownDuration;
        CooldownTimer = 0;
    }

    public void StartCooldown()
    {
        CooldownTimer = CooldownDuration;
    }

    public void Update(float deltaTime)
    {
        if (CooldownTimer > 0)
        {
            CooldownTimer -= deltaTime;
            CooldownTimer = Mathf.Max(CooldownTimer, 0);
        }
    }
    public float GetCooldownProgress()
    {
        if (CooldownDuration <= 0)
        {
            return 0;
        }
        return CooldownTimer / CooldownDuration;
    }
}