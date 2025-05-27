using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossAttack : BaseAttack
{
    private Transform target;
    [SerializeField] private DamageType damageType = DamageType.Physical;

    [Header("Glow Settings")]
    [SerializeField] private Renderer bossRenderer; 
    [SerializeField] private Color[] glowColors = new Color[4] { Color.red, Color.blue, Color.green, Color.yellow };
    [SerializeField] private float glowDuration = 0.5f;

    private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");

    public void SetTarget(Transform target)
    {
        this.target = target;
    }


    public override void PerformAttack()
    {
        if (IsOnCooldown || target == null) return;

        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, damageType);
            Debug.Log($"BossAttack: нанесён {damage} урон ({damageType}) цели: {target.name}");
        }

        // Запускаем свечение
        StartCoroutine(GlowRandomColor());
    }
    private IEnumerator GlowRandomColor()
    {
        if (bossRenderer == null || glowColors.Length < 4)
            yield break;

        Material material = bossRenderer.material; 

        material.EnableKeyword("_EMISSION");

        Color chosenColor = glowColors[Random.Range(0, glowColors.Length)];

        material.SetColor(EmissionColorID, chosenColor);

        yield return new WaitForSeconds(glowDuration);

        material.SetColor(EmissionColorID, Color.black);
    }
}
