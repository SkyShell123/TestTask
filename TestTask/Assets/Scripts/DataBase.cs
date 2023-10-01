using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{
    // Определяем список предметов
    public List<Item> items = new();
}

// Атрибут [System.Serializable] позволяет отображать класс в редакторе Unity
[System.Serializable]
public class Item
{
    // Уникальный идентификатор предмета
    public int id;

    // Название предмета
    public string name;

    // Спрайт (изображение) предмета
    public Sprite sprite;
}
