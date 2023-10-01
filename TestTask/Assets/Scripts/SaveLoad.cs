using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    void Start()
    {
        // Вызываем метод LoadData() при запуске приложения для загрузки данных, если они доступны.
        LoadData();
    }

    private void SaveData()
    {
        // Создаем объект PlayerData и заполняем его данными из различных компонентов игры.
        PlayerData playerData = new()
        {
            playerHealth = PlayerHealthSystem.Instance.SaveHealth(),
            playerAmmo = WeaponsGuns.Instance.SaveAmmoCount(),
            playerPoint = CharacterControls.Instance.SavePosition(),
            playerInventory = Inventory.Instance.SaveInventory(),
        };

        // Преобразуем объект PlayerData в формат JSON и сохраняем его в файл "playerData.json".
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText("playerData.json", json);
    }

    private void LoadData()
    {
        // Проверяем наличие файла "playerData.json".
        if (File.Exists("playerData.json"))
        {
            // Если файл существует, читаем его содержимое и десериализуем его в объект PlayerData.
            string json = File.ReadAllText("playerData.json");
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

            // Загружаем данные обратно в игровые компоненты.
            PlayerHealthSystem.Instance.LoadHealth(loadedData.playerHealth);
            WeaponsGuns.Instance.LoadAmmoCount(loadedData.playerAmmo);
            CharacterControls.Instance.LoadPosition(loadedData.playerPoint);
            Inventory.Instance.LoadInventory(loadedData.playerInventory);
        }
    }

    public void DeliteSaveFile()
    {
        // Проверяем наличие файла "playerData.json".
        if (File.Exists("playerData.json"))
        {
            // Если файл существует, удаляем его.
            File.Delete("playerData.json");
            Debug.Log("JSON файл успешно удален.");

            // Вызываем метод Die() у компонента CharacterControls для перезагрузки сцены.
            CharacterControls.Instance.Die();
        }
        else
        {
            Debug.LogWarning("JSON файл не найден.");
        }
    }

    private void OnApplicationQuit()
    {
        // Вызываем метод SaveData() при закрытии приложения для сохранения данных.
        SaveData();
    }
}
