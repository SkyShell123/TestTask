using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControls : MonoBehaviour
{
    // Создаем статическое свойство Instance, которое позволяет получить доступ к этому классу из других скриптов.
    public static CharacterControls Instance { get; private set; }

    public void Awake()
    {
        // Устанавливаем Instance равным текущему экземпляру этого скрипта.
        Instance = this;
    }

    // Метод для сохранения позиции персонажа.
    public Vector3 SavePosition()
    {
        // Возвращаем текущую позицию персонажа.
        return transform.position;
    }

    // Метод для загрузки позиции персонажа.
    public void LoadPosition(Vector3 point)
    {
        // Устанавливаем позицию персонажа в указанную точку.
        transform.position = point;
    }

    // Метод, вызываемый при смерти персонажа.
    public void Die()
    {
        // Перезагружаем текущую сцену, чтобы перезапустить игру.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
