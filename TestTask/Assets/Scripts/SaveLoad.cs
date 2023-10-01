using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    void Start()
    {
        // �������� ����� LoadData() ��� ������� ���������� ��� �������� ������, ���� ��� ��������.
        LoadData();
    }

    private void SaveData()
    {
        // ������� ������ PlayerData � ��������� ��� ������� �� ��������� ����������� ����.
        PlayerData playerData = new()
        {
            playerHealth = PlayerHealthSystem.Instance.SaveHealth(),
            playerAmmo = WeaponsGuns.Instance.SaveAmmoCount(),
            playerPoint = CharacterControls.Instance.SavePosition(),
            playerInventory = Inventory.Instance.SaveInventory(),
        };

        // ����������� ������ PlayerData � ������ JSON � ��������� ��� � ���� "playerData.json".
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText("playerData.json", json);
    }

    private void LoadData()
    {
        // ��������� ������� ����� "playerData.json".
        if (File.Exists("playerData.json"))
        {
            // ���� ���� ����������, ������ ��� ���������� � ������������� ��� � ������ PlayerData.
            string json = File.ReadAllText("playerData.json");
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

            // ��������� ������ ������� � ������� ����������.
            PlayerHealthSystem.Instance.LoadHealth(loadedData.playerHealth);
            WeaponsGuns.Instance.LoadAmmoCount(loadedData.playerAmmo);
            CharacterControls.Instance.LoadPosition(loadedData.playerPoint);
            Inventory.Instance.LoadInventory(loadedData.playerInventory);
        }
    }

    public void DeliteSaveFile()
    {
        // ��������� ������� ����� "playerData.json".
        if (File.Exists("playerData.json"))
        {
            // ���� ���� ����������, ������� ���.
            File.Delete("playerData.json");
            Debug.Log("JSON ���� ������� ������.");

            // �������� ����� Die() � ���������� CharacterControls ��� ������������ �����.
            CharacterControls.Instance.Die();
        }
        else
        {
            Debug.LogWarning("JSON ���� �� ������.");
        }
    }

    private void OnApplicationQuit()
    {
        // �������� ����� SaveData() ��� �������� ���������� ��� ���������� ������.
        SaveData();
    }
}
