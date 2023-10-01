using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    public EnemyAI enemyAI;     // Ссылка на компонент EnemyAI, который управляет врагом.
    public Transform RayDir;    // Направление луча, используемого для обнаружения препятствий.

    // Функция, выполняющая обнаружение объектов с указанным тегом методом луча.
    private bool ReyDetector(string tag)
    {
        // Создаем луч, выпущенный из позиции этого объекта в указанном направлении.
        RaycastHit2D HitInfo = Physics2D.Raycast(transform.position, RayDir.transform.right);

        // Если луч столкнулся с каким-то объектом:
        if (HitInfo.collider != null)
        {
            // Проверяем, имеет ли столкнувшийся объект указанный тег.
            if (HitInfo.transform.CompareTag(tag))
            {
                return true;  // Возвращаем true, если объект с указанным тегом обнаружен.
            }
            else
            {
                return false; // Возвращаем false, если объект не имеет указанный тег.
            }
        }
        else
        {
            return false; // Возвращаем false, если луч не столкнулся с объектом.
        }
    }

    // Метод, вызываемый при столкновении этого объекта с другим коллайдером.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Если столкнувшийся объект имеет тег "Player":
        if (other.CompareTag("Player"))
        {
            // Если луч не обнаруживает объект с тегом "Wall":
            if (!ReyDetector("Wall"))
            {
                // Вызываем метод PlayerDetected() из компонента EnemyAI.
                enemyAI.PlayerDetected();
            }
        }
    }
}
