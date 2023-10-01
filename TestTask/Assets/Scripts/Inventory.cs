using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<ItemInventory> items = new(); // Список предметов в инвентаре

    public DataBase data; // Ссылка на базу данных предметов

    public GameObject inventoryItemPrefab; // Префаб для отображения предметов в инвентаре

    public GameObject InventoryMainObj; // Главный объект инвентаря

    public int maxCount; // Максимальное количество ячеек инвентаря

    public Camera cam; // Ссылка на камеру для работы с позициями предметов

    public EventSystem eventSystem; // Ссылка на EventSystem для обработки событий в UI

    public Vector3 offset; // Смещение позиции предметов в инвентаре

    public GameObject backGround; // Объект заднего фона инвентаря

    public int maxItemCell; // Максимальное количество одного типа предметов в ячейке

    private void Awake()
    {
        Instance = this;

        AddGraphics(); // Создание графических элементов инвентаря

        UpdateInventory(); // Обновление отображения инвентаря
    }

    // Сериализуем инвентарь в список предметов для сохранения
    public List<Items> SaveInventory()
    {
        List<Items> _items = new();

        for (int i = 0; i < items.Count; i++)
        {
            Items newItem = new()
            {
                id = items[i].id,
                count = items[i].count
            };
            _items.Add(newItem);
        }

        return _items;
    }

    // Загружаем инвентарь из списка предметов
    public void LoadInventory(List<Items> _items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].id = _items[i].id;
            items[i].count = _items[i].count;
        }

        UpdateInventory();
    }

    // Попытка взять предмет и добавить его в инвентарь
    public bool TakeItem(Item item)
    {
        if (AddItem(SeachSameItem(item), item))
        {
            return true;
        }
        return false;
    }

    // Открыть/закрыть инвентарь
    public void BackPack()
    {
        backGround.SetActive(!backGround.activeSelf);
        if (backGround.activeSelf)
        {
            UpdateInventory();
        }
    }

    // Поиск ячейки с предметом того же типа или пустой ячейки
    public int SeachSameItem(Item item)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id == item.id)
            {
                if (items[i].count < maxItemCell)
                {
                    return i;
                }
            }
        }

        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id == 0)
            {
                return i;
            }
        }

        return -1;
    }

    // Добавить предмет в инвентарь по указанной ячейке
    public bool AddItem(int id, Item item)
    {
        if (id != -1)
        {
            items[id].id = item.id;
            items[id].count++;
            items[id].itemObj.GetComponent<Image>().sprite = item.sprite;

            UpdateInventory();
        }
        else
        {
            return false;
        }
        return true;
    }

    // Создание графических элементов инвентаря
    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(inventoryItemPrefab, InventoryMainObj.transform);

            newItem.name = i.ToString();

            ItemInventory inventory = new ItemInventory()
            {
                itemObj = newItem
            };

            RectTransform rect = newItem.GetComponent<RectTransform>();
            rect.localPosition = new Vector3(0, 0, 0);
            rect.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button button = newItem.GetComponent<Button>();
            Button subButton = newItem.transform.GetChild(1).GetComponent<Button>();

            button.onClick.AddListener(delegate { SubButton(subButton.gameObject); });
            subButton.onClick.AddListener(delegate { DeliteItem(int.Parse(button.name)); });

            items.Add(inventory);
        }
    }

    // Обработчик нажатия на кнопку "Убрать" для предмета
    public void SubButton(GameObject subButton)
    {
        int id = int.Parse(subButton.transform.parent.name);

        if (items[id].count > 0)
        {
            subButton.SetActive(!subButton.activeSelf);
        }
    }

    // Удаление предмета из инвентаря по указанной ячейке
    public void DeliteItem(int id)
    {
        if (items[id].count > 0)
        {
            items[id].count--;

            if (items[id].count == 0)
            {
                GameObject subButton = items[id].itemObj.transform.GetChild(1).gameObject;
                subButton.SetActive(false);
            }
        }

        UpdateInventory();
    }

    // Обновление отображения инвентаря
    public void UpdateInventory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id != 0 && items[i].count > 1)
            {
                items[i].itemObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemObj.GetComponentInChildren<Text>().text = "";
            }
            if (items[i].count <= 0)
            {
                items[i].id = 0;

                items[i].itemObj.GetComponent<Image>().sprite = data.items[0].sprite;
            }

            items[i].itemObj.GetComponent<Image>().sprite = data.items[items[i].id].sprite;
        }
    }
}

[Serializable]
public class ItemInventory
{
    public int id;
    public GameObject itemObj;
    public int count;
}

[Serializable]
public class Items
{
    public int id;
    public int count;
}