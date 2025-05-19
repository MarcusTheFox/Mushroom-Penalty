using System;
using UnityEngine;

public interface IAttack
{
    public float Damage { get; }
    public event Action OnReady;
    public event Action OnStart;
    public event Action OnApply;
    public event Action OnStop;

    public void Ready();
    public void Start();
    public void Apply();
    public void Stop();
}
