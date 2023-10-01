using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    // Переменная для хранения количества урона, который наносит этот объект
    public int damage = 1;

    // Этот метод вызывается, когда объект сталкивается с другим коллайдером
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, имеет ли столкнувшийся объект метку "Player"
        if (other.CompareTag("Player"))
        {
            // Получаем доступ к компоненту PlayerHealthSystem на родительском объекте
            PlayerHealthSystem player = other.GetComponentInParent<PlayerHealthSystem>();

            // Вызываем метод TakeDamage у объекта player, передавая ему урон
            player.TakeDamage(damage);
        }
    }
}
