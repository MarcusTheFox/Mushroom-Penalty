using UnityEngine;

public class BossStrongAttack : BaseAttack
{
    private Transform target;
    [SerializeField] private DamageType damageType = DamageType.Fire;

    [Header("Strong Attack Visuals")]
    [SerializeField] private ParticleSystem[] strongAttackVFXs; // массив из 4 ParticleSystem

    public void SetTarget(Transform target)
    {
        this.target = target;
    }


    public override void PerformAttack()
    {
        if (IsOnCooldown || target == null) return;

        // Визуальный эффект
        PlayOnlyOneVFX();


        // Нанесение урона
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, damageType);
            Debug.Log($"BossStrongAttack: нанесён {damage} урон ({damageType}) цели: {target.name}");
        }


    }

    private void PlayOnlyOneVFX()
    {
        if (strongAttackVFXs == null || strongAttackVFXs.Length == 0)
            return;

        // Остановить все эффекты перед запуском нового
        foreach (var vfx in strongAttackVFXs)
        {
            if (vfx != null)
            {
                vfx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                vfx.Clear();
            }
        }

        // Выбрать случайный и проиграть
        int index = Random.Range(0, strongAttackVFXs.Length);
        var selectedVFX = strongAttackVFXs[index];

        if (selectedVFX != null)
        {
            selectedVFX.Play();
        }
    }


}
