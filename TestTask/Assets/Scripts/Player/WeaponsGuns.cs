using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsGuns : MonoBehaviour
{
    public static WeaponsGuns Instance { get; private set; }

    // ������ �� ��������� ���� ��� ����������� �����������
    public Text AmmoMenu;

    // ������ �� �������, ������ ����� ����������� ����
    public Transform ShotDir;

    // ������ ����
    public GameObject Bullet;

    // ������� ���������� �����������
    private float AmmoCount;

    // ������������ ���������� �����������
    private float MaxAmmoCount;

    // ��������� ���������� ����������� ��� �������� �������
    public float AmmoMemory;

    // ������������ ���������� ����������� ��� �������� �������
    public float MaxAmmoMemory;

    // ����, ��������� ������
    public int Damage = 5;

    // ����� ����� ����������
    public float timeShot;

    // ��������� ����� ����� ����������
    public float startTime = 0.5f;

    // �������� ������ ����
    public float SpeedShoot = 10f;

    // ����� ����� ����
    public float destroyTime = 1f;

    // ���� ��� �����������, ���������� �� ������� �����������
    private bool recharge;

    // ����� ������� �����������
    public float rechargeTime = 2f;

    public void Awake()
    {
        Instance = this;

        // �������������� ���������� ����������� � ������������ ����������
        AmmoCount = AmmoMemory;
        MaxAmmoCount = MaxAmmoMemory;

        // ��������� ����������� �����������
        UpdateGun();
    }

    // ����� ��� ���������� ��������� �����������
    public float[] SaveAmmoCount()
    {
        return new float[] { AmmoCount, MaxAmmoCount };
    }

    // ����� ��� �������� ��������� �����������
    public void LoadAmmoCount(float[] _AmmoCount)
    {
        AmmoCount = _AmmoCount[0];
        MaxAmmoCount = _AmmoCount[1];

        UpdateGun();
    }

    void Update()
    {
        // ���� ����������� ������ 1 � �� ���������� ������� � ���� �������� ����������
        if (AmmoCount < 1 && !recharge && MaxAmmoCount > 0)
        {
            // �������� �������
            recharge = true;
            Invoke(nameof(Recharge), rechargeTime);
        }
        else
        {
            // ��������� ����� �� ���������� ��������, ���� ��� ������ 0
            if (timeShot > 0)
            {
                timeShot -= Time.deltaTime;
            }
        }
    }

    // ����� ��� ���������� ��������, ���������� ����� ������ � ��������� ������
    public void Fire()
    {
        // ���� ����� �� ���������� �������� ������, ���� ���������� � �� ���� �������
        if (timeShot <= 0 && AmmoCount > 0 && !recharge)
        {
            // ��������� ����
            Spawn();

            // ������������� ����� �� ���������� ��������
            timeShot = startTime;

            // ��������� ���������� �����������
            AmmoCount -= 1;

            // ��������� ����������� �����������
            UpdateGun();
        }
    }

    // ����� ��� ����������� �����������
    void Recharge()
    {
        // ������������� ���������� ����������� � ������������ ��������
        AmmoCount = AmmoMemory;

        // ��������� ������������ ���������� �����������
        MaxAmmoCount -= AmmoMemory;

        // ��������� �����������
        recharge = false;

        // ��������� ����������� �����������
        UpdateGun();
    }

    // ����� ��� �������� ����
    void Spawn()
    {
        // ������� ��������� ����
        GameObject inst_obj = Instantiate(Bullet, ShotDir.position, ShotDir.rotation);

        // ������������� ����� ����� � ���� ��� ����
        Bullet bullet = inst_obj.GetComponent<Bullet>();
        bullet.destroyTime = destroyTime;
        bullet.damage = Damage;

        // ��������� ���� ��� �������� ����
        Rigidbody2D rb = inst_obj.GetComponent<Rigidbody2D>();
        rb.AddForce(ShotDir.up * SpeedShoot, ForceMode2D.Impulse);
    }

    // ����� ��� ���������� ����������� �����������
    private void UpdateGun()
    {
        AmmoMenu.text = $"{AmmoCount} / {MaxAmmoCount}";
    }
}
