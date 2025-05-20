using System;

public interface ICooldown
{
    float Duration { get; }
    bool IsActive { get; }
    bool IsInvalidated { get; }

    float ElapsedTime { get; }
    float TimeRemaining { get; }
    float ProgressNormalized { get; }
    
    public event Action OnStart;
    public event Action OnUpdate;
    public event Action OnFinish;
    public event Action OnInvalidate;
    
    public void Start();
    public void Update();
    public void Stop();
    public void ClearAndInvalidate();
}
