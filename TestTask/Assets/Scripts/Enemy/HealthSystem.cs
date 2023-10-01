using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    // ����������, ��������� ��� ��������� � ��������� Unity.
    public Image bar;           // ������ �� ��������� Image ��� ����������� ������ ��������.
    public float health;        // ������� �������� ���������.
    private float healthMemory; // ���������� ��� �������� ���������� �������� ��������.

    private void Start()
    {
        // ��� ������� ���� ��������� ��������� �������� �������� � ��������� �����������.
        healthMemory = health;
        UpdateHealth();
    }

    // ����� ��� ���������� �������� ��������� ��� ��������� �����.
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }

        // ��������� ����������� ��������.
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        // ��������� fillAmount ���������� Image, ����� �������� ������� �������� ������������ ���������� ��������.
        bar.fillAmount = health / healthMemory;
    }

    // ����� ���������� ��� ������ ���������.
    void Die()
    {
        // �������� ��������� EnemyAI, �����������, ��� ���� ������ ����������� � �����, � �������� ��� ����� Die.
        EnemyAI enemy = GetComponent<EnemyAI>();
        enemy.Die();
    }
}
