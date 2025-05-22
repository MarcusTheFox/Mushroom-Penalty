using System;
using UnityEngine;
using View;
using Object = UnityEngine.Object;

public class MagicAttack: IAttack
{
    public float Damage { get; }
    public event Action OnReady;
    public event Action OnStart;
    public event Action OnApply;
    public event Action OnStop;
    
    private ICooldown cooldown;
    private readonly LayerMask targetLayer;
    private readonly GameObject fireballPrefab;
    private readonly Transform fireballSpawnPoint;
    private readonly Transform playerTransform;

    private bool cooldownFinished;
    private bool attackEnded;
    private bool isReady;
    
    public MagicAttack(float damage, ICooldown cooldown, LayerMask targetLayer, GameObject fireballPrefab,
        Transform fireballSpawnPoint, Transform playerTransform)
    {
        Damage = damage;
        this.cooldown = cooldown;
        this.targetLayer = targetLayer;
        this.fireballPrefab = fireballPrefab;
        this.fireballSpawnPoint = fireballSpawnPoint;
        this.playerTransform = playerTransform;

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
        
        GameObject fireball =
            Object.Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
        
        FireballObjectInitializer fireballComponent = fireball.GetComponent<FireballObjectInitializer>();

        if (fireballComponent != null)
        {
            fireballComponent.AddSettings(playerTransform.gameObject, fireballSpawnPoint.forward, 10f);
        }
        
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
