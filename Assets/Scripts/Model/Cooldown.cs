using System;
using UnityEngine;

public class Cooldown: ICooldown
{
    public float Duration { get; }
    public bool IsActive { get; private set; }
    public event Action OnStart;
    public event Action OnFinished;
    
    private float timer;

    public Cooldown(float duration)
    {
        Duration = duration;
        IsActive = false;
    }
    
    public void Start()
    {
        timer = Time.time;
        IsActive = true;
        OnStart?.Invoke();
    }

    public void Update()
    {
        if (Time.time - timer > Duration)
        {
            Stop();
        }
    }

    public void Stop()
    {
        IsActive = false;
        OnFinished?.Invoke();
    }
}
