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
    
    public MagicAttack(float damage, ICooldown cooldown)
    {
        Damage = damage;
        this.cooldown = cooldown;
    }
    
    public void Ready()
    {
        OnReady?.Invoke();
    }

    public void Start()
    {
        OnStart?.Invoke();
    }

    public void Apply()
    {
        OnApply?.Invoke();
    }

    public void Stop()
    {
        OnStop?.Invoke();
    }
}
