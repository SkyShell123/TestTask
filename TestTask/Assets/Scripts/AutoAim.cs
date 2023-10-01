using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public AimController additionalObject; // Ссылка на AimController, который будет управлять дополнительным объектом
    private AimController aimController; // Ссылка на AimController, присоединенный к этому объекту
    private GameObject target; // Цель для автоматического прицеливания

    void Start()
    {
        aimController = GetComponent<AimController>(); // Получаем ссылку на AimController при запуске сцены
    }

    void Update()
    {
        if (target != null)
        {
            OnAutoAim(); // Если есть цель, выполняем автоматическое прицеливание
        }
    }

    private void OnAutoAim()
    {
        // Рассчитываем направление к цели и устанавливаем соответствующий угол поворота для объекта
        Vector3 direction = new(target.transform.position.x, target.transform.position.y, 0f);
        Vector3 movePosition = direction - transform.position;
        float rotateZ = Mathf.Atan2(movePosition.y, movePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotateZ);

        // Если есть дополнительный объект, устанавливаем ему такой же угол поворота
        if (additionalObject != null)
        {
            additionalObject.gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotateZ);
        }
    }

    private void OffAutoAim()
    {
        aimController.enabled = true; // Включаем AimController при завершении автоматического прицеливания
        if (additionalObject != null)
        {
            additionalObject.enabled = true; // Включаем дополнительный AimController (если есть) при завершении автоматического прицеливания
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && target == null)
        {
            target = other.gameObject; // Устанавливаем новую цель, если объект с тегом "Enemy" входит в зону действия
            aimController.enabled = false; // Отключаем AimController при входе в зону действия
            if (additionalObject != null)
            {
                additionalObject.enabled = false; // Отключаем дополнительный AimController (если есть) при входе в зону действия
            }
            OnAutoAim(); // Выполняем автоматическое прицеливание
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (target != null && other.gameObject == target)
        {
            target = null; // Сбрасываем цель, когда объект покидает зону действия
            OffAutoAim(); // Завершаем автоматическое прицеливание и включаем AimController
        }
    }
}
