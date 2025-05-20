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
    
    public MagicAttack(float damage, ICooldown cooldown)
    {
        Damage = damage;
        this.cooldown = cooldown;
        
        cooldown.OnFinish += OnCooldownFinish;
        cooldown.OnInvalidate += OnCooldownInvalidate;
    }
    
    public void Ready()
    {
        if (cooldown.IsInvalidated || cooldown.IsActive) return;
        
        Debug.Log("Magic Attack Ready");
        OnReady?.Invoke();
    }

    public void Start()
    {
        if (cooldown.IsInvalidated || cooldown.IsActive) return;
        
        cooldown.Start();
        attackEnded = false;
        cooldownFinished = false;
        
        Debug.Log("Magic Attack Started");
        OnStart?.Invoke();
    }

    public void Apply()
    {
        if (cooldown.IsInvalidated) return;
        
        Debug.Log("Magic Attack Applied");
        OnApply?.Invoke();
    }

    public void Stop()
    {
        if (attackEnded) return;
        
        attackEnded = true;
        
        Debug.Log("Magic Attack Stopped");
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
