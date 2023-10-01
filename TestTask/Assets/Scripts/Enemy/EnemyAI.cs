using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Animator animator; // Ссылка на компонент анимации для управления анимациями врага.
    public float speed = 10f; // Скорость передвижения врага.
    private GameObject player; // Ссылка на объект игрока.
    public bool isAttack; // Флаг, указывающий, находится ли враг в состоянии атаки.

    private bool playerDetected; // Флаг, указывающий, что игрок обнаружен в зоне видимости врага.
    public float hitDistance = 2f; // Расстояние, на котором враг начинает атаку.

    private void Start()
    {
        animator = GetComponent<Animator>(); // Получаем компонент анимации при запуске.
        player = GameObject.FindGameObjectWithTag("Player"); // Находим игрока по тегу "Player".
    }

    public void PlayerDetected()
    {
        playerDetected = true; // Вызывается, когда игрок обнаружен в зоне видимости врага.
    }

    private void Update()
    {
        if (playerDetected)
        {
            Move(); // Если игрок обнаружен, вызываем функцию перемещения врага.
        }
    }

    public void Move()
    {
        // Поворот врага в направлении игрока в зависимости от его положения.
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Поворачиваем врага влево.
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Поворачиваем врага вправо.
        }

        // Перемещение врага к игроку, если расстояние больше заданной дистанции атаки.
        if (Vector3.Distance(transform.position, player.transform.position) > hitDistance)
        {
            if (!isAttack)
            {
                animator.SetBool("IsAttacking", false); // Убеждаемся, что атака выключена.
                animator.SetBool("EnemyWalk", true); // Включаем анимацию ходьбы.
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
        else
        {
            if (!isAttack)
            {
                animator.SetBool("EnemyWalk", false); // Выключаем анимацию ходьбы.
                animator.SetBool("IsAttacking", true); // Включаем анимацию атаки.
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject); // Уничтожаем объект врага при его смерти.
        EnemyController.Instance.Drop(transform); // Вызываем метод Drop() из класса EnemyController для дропа предметов.
    }
}
