using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{
    // ���������� ������ ���������
    public List<Item> items = new();
}

// ������� [System.Serializable] ��������� ���������� ����� � ��������� Unity
[System.Serializable]
public class Item
{
    // ���������� ������������� ��������
    public int id;

    // �������� ��������
    public string name;

    // ������ (�����������) ��������
    public Sprite sprite;
}
