using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFind : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, если объект, с которым столкнулся данный объект, имеет метку "Loot"
        if (other.gameObject.CompareTag("Loot"))
        {
            // Получаем компонент Drop из объекта other
            Drop drop = other.GetComponent<Drop>();

            // Пытаемся взять предмет в инвентарь, используя метод TakeItem из класса Inventory.Instance
            if (Inventory.Instance.TakeItem(drop.item))
            {
                // Если предмет был успешно взят, уничтожаем объект other
                Destroy(other.gameObject);
            }
        }
    }
}
