using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    internal float destroyTime; // Время, через которое пуля будет уничтожена после запуска.
    internal int damage; // Урон, наносимый пулей.

    private void Start()
    {
        // Устанавливаем таймер на уничтожение пули после заданного времени.
        Invoke(nameof(DestroyBullet), destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Вызывается, когда объект пули сталкивается с другим коллайдером.
        if (other.CompareTag("Enemy"))
        {
            // Если столкнулись с объектом, имеющим тег "Enemy".
            // Получаем компонент HealthSystem из родительского объекта,
            // чтобы нанести урон врагу и вызвать его метод TakeDamage().
            HealthSystem enemy = other.GetComponentInParent<HealthSystem>();
            enemy.TakeDamage(damage);
            DestroyBullet(); // Уничтожаем пулю после нанесения урона.
        }
        else if (other.CompareTag("Wall"))
        {
            // Если столкнулись с объектом, имеющим тег "Wall", то просто уничтожаем пулю.
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        // Уничтожает объект пули.
        Destroy(gameObject);
    }
}
