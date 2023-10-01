using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<ItemInventory> items = new(); // ������ ��������� � ���������

    public DataBase data; // ������ �� ���� ������ ���������

    public GameObject inventoryItemPrefab; // ������ ��� ����������� ��������� � ���������

    public GameObject InventoryMainObj; // ������� ������ ���������

    public int maxCount; // ������������ ���������� ����� ���������

    public Camera cam; // ������ �� ������ ��� ������ � ��������� ���������

    public EventSystem eventSystem; // ������ �� EventSystem ��� ��������� ������� � UI

    public Vector3 offset; // �������� ������� ��������� � ���������

    public GameObject backGround; // ������ ������� ���� ���������

    public int maxItemCell; // ������������ ���������� ������ ���� ��������� � ������

    private void Awake()
    {
        Instance = this;

        AddGraphics(); // �������� ����������� ��������� ���������

        UpdateInventory(); // ���������� ����������� ���������
    }

    // ����������� ��������� � ������ ��������� ��� ����������
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

    // ��������� ��������� �� ������ ���������
    public void LoadInventory(List<Items> _items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].id = _items[i].id;
            items[i].count = _items[i].count;
        }

        UpdateInventory();
    }

    // ������� ����� ������� � �������� ��� � ���������
    public bool TakeItem(Item item)
    {
        if (AddItem(SeachSameItem(item), item))
        {
            return true;
        }
        return false;
    }

    // �������/������� ���������
    public void BackPack()
    {
        backGround.SetActive(!backGround.activeSelf);
        if (backGround.activeSelf)
        {
            UpdateInventory();
        }
    }

    // ����� ������ � ��������� ���� �� ���� ��� ������ ������
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

    // �������� ������� � ��������� �� ��������� ������
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

    // �������� ����������� ��������� ���������
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

    // ���������� ������� �� ������ "������" ��� ��������
    public void SubButton(GameObject subButton)
    {
        int id = int.Parse(subButton.transform.parent.name);

        if (items[id].count > 0)
        {
            subButton.SetActive(!subButton.activeSelf);
        }
    }

    // �������� �������� �� ��������� �� ��������� ������
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

    // ���������� ����������� ���������
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