using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSistem : MonoBehaviour
{
    public Image bar;
    public float health;
    private float healthMemory;
    //SpriteRenderer spriteRenderer;

    private void Start()
    {
        healthMemory = health;
        //spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateHealth();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        //spriteRenderer.color = Color.red;
        Invoke(nameof(DamageEffect), 0.2f);

        UpdateHealth();
    }

    void DamageEffect()
    {
        //spriteRenderer.color = Color.white;

        if (health <= 0)
        {
            //spriteRenderer.color = Color.red;
            Invoke(nameof(Die), 0.1f);
        }
    }

    private void UpdateHealth()
    {
        bar.fillAmount = health / healthMemory;
    }

    void Die()
    {
        EnemyAI enemy = GetComponent<EnemyAI>();
        enemy.Die();
    }
}
