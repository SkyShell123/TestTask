using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public static PlayerHealthSystem Instance { get; private set; }

    // Ссылка на изображение (полосу здоровья), которая будет обновляться.
    public Image bar;

    // Значение здоровья по умолчанию.
    public float healthMemory = 1f;

    // Здоровье игрока.
    private float health;

    public void Awake()
    {
        Instance = this;

        // Инициализация здоровья игрока.
        health = healthMemory;
        UpdateHealth();
    }

    // Сохранение текущего значения здоровья.
    public float SaveHealth()
    {
        return health;
    }

    // Загрузка значения здоровья из сохраненных данных.
    public void LoadHealth(float _health)
    {
        health = _health;

        // Обновление отображения здоровья.
        UpdateHealth();
    }

    // Вычитание урона из здоровья.
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // Если здоровье достигло нуля или меньше, вызываем метод Die().
            Die();
        }

        UpdateHealth();
    }

    // Обновление полосы здоровья на экране.
    private void UpdateHealth()
    {
        // Устанавливаем fillAmount (заполненность) изображения в соответствии с текущим здоровьем.
        bar.fillAmount = health / healthMemory;
    }

    // Вызывается, когда здоровье игрока опускается до нуля или меньше.
    void Die()
    {
        // Вызываем метод Die() из другого класса (CharacterControls).
        CharacterControls.Instance.Die();
    }
}
