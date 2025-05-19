using System;

public interface ICooldown
{
    public float Duration { get; }
    public bool IsActive { get; }
    public event Action OnStart;
    public event Action OnFinished;
    
    public void Start();
    public void Update();
    public void Stop();
}
