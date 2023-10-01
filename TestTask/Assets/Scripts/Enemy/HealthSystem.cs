using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    // Переменные, доступные для настройки в редакторе Unity.
    public Image bar;           // Ссылка на компонент Image для отображения полосы здоровья.
    public float health;        // Текущее здоровье персонажа.
    private float healthMemory; // Переменная для хранения начального значения здоровья.

    private void Start()
    {
        // При запуске игры сохраняем начальное значение здоровья и обновляем отображение.
        healthMemory = health;
        UpdateHealth();
    }

    // Метод для уменьшения здоровья персонажа при получении урона.
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }

        // Обновляем отображение здоровья.
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        // Обновляем fillAmount компонента Image, чтобы отразить текущее здоровье относительно начального значения.
        bar.fillAmount = health / healthMemory;
    }

    // Метод вызывается при смерти персонажа.
    void Die()
    {
        // Получаем компонент EnemyAI, предполагая, что этот скрипт применяется к врагу, и вызываем его метод Die.
        EnemyAI enemy = GetComponent<EnemyAI>();
        enemy.Die();
    }
}
