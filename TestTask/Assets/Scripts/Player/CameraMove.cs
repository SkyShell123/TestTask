using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Параметр для сглаживания движения камеры
    public float dumping = 1.5f;

    // Смещение камеры относительно игрока
    public Vector2 offset = new(2f, 1f);

    // Флаг, указывающий на то, в какую сторону смотрит персонаж
    public bool isLeft;

    // Ссылка на модель игрока
    public Transform playerModel;

    // Переменная для хранения последней позиции игрока по X
    private int lastX;

    void Start()
    {
        // Убедимся, что значения смещения не отрицательные
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);

        // Находим игрока на старте
        FindPlayer(isLeft);
    }

    void FindPlayer(bool playerIsLeft)
    {
        // Запоминаем позицию игрока по X
        lastX = Mathf.RoundToInt(playerModel.position.x);

        if (playerIsLeft)
        {
            // Устанавливаем начальную позицию камеры слева от игрока
            transform.position = new Vector3(playerModel.position.x - offset.x, playerModel.position.y - offset.y, transform.position.z);
        }
        else
        {
            // Устанавливаем начальную позицию камеры справа от игрока
            transform.position = new Vector3(playerModel.position.x + offset.x, playerModel.position.y + offset.y, transform.position.z);
        }
    }

    void Update()
    {
        if (playerModel)
        {
            // Запоминаем текущую позицию игрока по X
            int currentX = Mathf.RoundToInt(playerModel.position.x);

            // Определяем, в какую сторону смотрит игрок
            if (currentX > lastX) isLeft = false;
            else if (currentX < lastX) isLeft = true;

            // Запоминаем текущую позицию игрока
            lastX = Mathf.RoundToInt(playerModel.position.x);

            Vector3 target;
            if (isLeft)
            {
                // Задаем целевую позицию камеры слева от игрока
                target = new Vector3(playerModel.position.x - offset.x, playerModel.position.y + offset.y, transform.position.z);
            }
            else
            {
                // Задаем целевую позицию камеры справа от игрока
                target = new Vector3(playerModel.position.x + offset.x, playerModel.position.y + offset.y, transform.position.z);
            }

            // Сглаживаем движение камеры к целевой позиции
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}
