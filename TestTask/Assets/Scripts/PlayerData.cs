using System;
using System.Collections.Generic;
using UnityEngine;

// Объявление класса PlayerData, который будет сериализован (может быть сохранен и загружен).
[Serializable]
public class PlayerData
{
    // Поле для хранения здоровья игрока.
    public float playerHealth;

    // Массив для хранения количества патронов игрока.
    public float[] playerAmmo;

    // Векторная переменная для хранения позиции игрока в игровом мире.
    public Vector3 playerPoint;

    // Список (коллекция) предметов, находящихся в инвентаре игрока.
    public List<Items> playerInventory;
}
