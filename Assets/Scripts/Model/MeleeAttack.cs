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
        Debug.Log("Melee Attack Ready");
        OnReady?.Invoke();
    }

    public void Start()
    {
        Debug.Log("Melee Attack Started");
        OnStart?.Invoke();
    }

    public void Apply()
    {
        Debug.Log("Melee Attack Applied");
        OnApply?.Invoke();
    }

    public void Stop()
    {
        Debug.Log("Melee Attack Stopped");
        OnStop?.Invoke();
        Ready();
    }
}
