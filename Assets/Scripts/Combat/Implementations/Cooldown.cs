using System;
using Combat.Interfaces;
using UnityEngine;

namespace Combat.Implementations
{
    public class Cooldown: ICooldown
    {
        public float Duration { get; }
        public bool IsActive { get; private set; }
        public bool IsInvalidated { get; private set; }
        public float ElapsedTime
        {
            get
            {
                if (startTime < 0 || IsInvalidated) return 0f;
                if (!IsActive) return Duration;
            
                return Time.time - startTime;
            }
        }

        public float TimeRemaining
        {
            get
            {
                if (!IsActive || IsInvalidated) return 0f;
            
                return Mathf.Max(0f, Duration - (Time.time - startTime));
            }
        }

        public float ProgressNormalized
        {
            get
            {
                if (IsInvalidated) return 0f;
                if (!IsActive || Duration == 0f) return 1f;
            
                return Mathf.Clamp01((Time.time - startTime) / Duration);
            }
        }

        public event Action OnStart;
        public event Action OnUpdate;
        public event Action OnFinish;
        public event Action OnInvalidate;

        private float startTime;

        public Cooldown(float duration)
        {
            Duration = Mathf.Max(0, duration);
            IsActive = false;
            startTime = -1f;
        }
    
        public void Start()
        {
            if (IsActive || IsInvalidated) return;
        
            IsActive = true;
            startTime = Time.time;
            OnStart?.Invoke();
        }

        public void Update()
        {
            if (!IsActive || IsInvalidated) return;
        
            if (Time.time - startTime > Duration)
            {
                Stop();
            }
        
            OnUpdate?.Invoke();
        }

        public void Stop()
        {
            if (!IsActive) return;
        
            IsActive = false;
            OnFinish?.Invoke();
        }

        public void ClearAndInvalidate()
        {
            if (IsInvalidated) return;
        
            Invalidate();
            ClearAllEventHandlers();
        }

        private void Invalidate()
        {
            IsActive = false;
            IsInvalidated = true;
            OnInvalidate?.Invoke();
        }

        private void ClearAllEventHandlers()
        {
            OnStart = null;
            OnUpdate = null;
            OnFinish = null;
            OnInvalidate = null;
        }
    }
}
