using System;
using UnityEngine;

public class MeleeAttack: IAttack
{
    public float Damage { get; }
    public event Action OnReady;
    public event Action OnStart;
    public event Action OnApply;
    public event Action OnStop;

    public MeleeAttack(float damage)
    {
        Damage = damage;
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
        Ready();
    }
}
