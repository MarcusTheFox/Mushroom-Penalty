using System;
using UnityEngine;

public class MagicAttack: IAttack
{
    public float Damage { get; }
    public event Action OnReady;
    public event Action OnStart;
    public event Action OnApply;
    public event Action OnStop;
    
    private ICooldown cooldown;
    
    private bool cooldownFinished;
    private bool attackEnded;
    private bool isReady;
    
    public MagicAttack(float damage, ICooldown cooldown)
    {
        Damage = damage;
        this.cooldown = cooldown;
        
        isReady = true;
        
        cooldown.OnFinish += OnCooldownFinish;
        cooldown.OnInvalidate += OnCooldownInvalidate;
    }
    
    public void Ready()
    {
        if (cooldown.IsInvalidated || cooldown.IsActive) return;
        
        isReady = true;
        OnReady?.Invoke();
    }

    public void Start()
    {
        if (!isReady || cooldown.IsActive || cooldown.IsInvalidated) return;
        
        isReady = false;
        cooldown.Start();
        attackEnded = false;
        cooldownFinished = false;
        
        OnStart?.Invoke();
    }

    public void Apply()
    {
        if (cooldown.IsInvalidated) return;
        
        OnApply?.Invoke();
    }

    public void Stop()
    {
        if (attackEnded) return;
        
        attackEnded = true;
        
        OnStop?.Invoke();

        if (attackEnded && cooldownFinished)
        {
            Ready();
        }
    }

    private void OnCooldownFinish()
    {
        cooldownFinished = true;

        if (attackEnded && cooldownFinished)
        {
            Ready();
        }
    }

    private void OnCooldownInvalidate()
    {
        cooldown.OnFinish -= OnCooldownFinish;
        cooldown.OnInvalidate -= OnCooldownInvalidate;
    }
}
