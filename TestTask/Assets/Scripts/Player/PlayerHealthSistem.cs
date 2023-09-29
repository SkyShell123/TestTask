using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSistem : MonoBehaviour
{
    public static PlayerHealthSistem Instance { get; private set; }
    //public SpriteRender spriteRender;
    public Image bar;
    public float healthMemory = 1f;
    private float health;

    public void Awake()
    {
        Instance = this;

        health = healthMemory;
        UpdateHealth();
    }

    public float SaveHealth()
    {
        return health;
    }

    public void LoadHealth(float _health)
    {
        health = _health;

        UpdateHealth();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        //spriteRender.ChangeSpriteColor(Color.red);
        Invoke(nameof(DamageEffect), 0.2f);

        UpdateHealth();
    }

    void DamageEffect()
    {
        //spriteRender.ChangeSpriteColor(Color.white);

        if (health <= 0)
        {
            //spriteRender.ChangeSpriteColor(Color.red);
            Invoke(nameof(Die), 0.1f);

        }
    }

    public void FullHealth()
    {
        health = healthMemory;
        UpdateHealth();
    }

    private void UpdateHealth() 
    {
        bar.fillAmount = health / healthMemory;
    }

    void Die()
    {
        CharacterControls.Instance.Die();
    }
}
