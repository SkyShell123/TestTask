using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        LoadData();

    }

    private void SaveData()
    {
        PlayerData playerData = new()
        {
            playerHealth = PlayerHealthSistem.Instance.SaveHealth(),
            playerAmmo = WeaponsGuns.Instance.SaveAmmoCount(),
            playerPoint = CharacterControls.Instance.SavePosition(),
            //playerInventory = Inventory.Instance.SaveInventory(),
        };

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText("playerData.json", json);
    }

    private void LoadData()
    {
        if (File.Exists("playerData.json"))
        {
            string json = File.ReadAllText("playerData.json");
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

            PlayerHealthSistem.Instance.LoadHealth(loadedData.playerHealth);
            WeaponsGuns.Instance.LoadAmmoCount(loadedData.playerAmmo);
            CharacterControls.Instance.LoadPosition(loadedData.playerPoint);
            //Inventory.Instance.LoadInventory(loadedData.playerInventory);
        }
        
    }

    public void DeliteSaveFile()
    {

        if (File.Exists("playerData.json"))
        {
            File.Delete("playerData.json");
            Debug.Log("JSON файл успешно удален.");
            CharacterControls.Instance.Die();
        }
        else
        {
            Debug.LogWarning("JSON файл не найден.");
        }
    }

    private void OnApplicationQuit()
    {
        // Сохраните данные при выходе из игры
        SaveData();
    }
}
