using System;
using System.Collections.Generic;
using UnityEngine;

// ���������� ������ PlayerData, ������� ����� ������������ (����� ���� �������� � ��������).
[Serializable]
public class PlayerData
{
    // ���� ��� �������� �������� ������.
    public float playerHealth;

    // ������ ��� �������� ���������� �������� ������.
    public float[] playerAmmo;

    // ��������� ���������� ��� �������� ������� ������ � ������� ����.
    public Vector3 playerPoint;

    // ������ (���������) ���������, ����������� � ��������� ������.
    public List<Items> playerInventory;
}
