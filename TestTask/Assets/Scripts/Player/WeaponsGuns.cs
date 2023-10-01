using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsGuns : MonoBehaviour
{
    public static WeaponsGuns Instance { get; private set; }

    // Ссылка на текстовое поле для отображения боеприпасов
    public Text AmmoMenu;

    // Ссылка на позицию, откуда будут выпускаться пули
    public Transform ShotDir;

    // Префаб пули
    public GameObject Bullet;

    // Текущее количество боеприпасов
    private float AmmoCount;

    // Максимальное количество боеприпасов
    private float MaxAmmoCount;

    // Начальное количество боеприпасов при создании объекта
    public float AmmoMemory;

    // Максимальное количество боеприпасов при создании объекта
    public float MaxAmmoMemory;

    // Урон, наносимый пулями
    public int Damage = 5;

    // Время между выстрелами
    public float timeShot;

    // Начальное время между выстрелами
    public float startTime = 0.5f;

    // Скорость полета пуль
    public float SpeedShoot = 10f;

    // Время жизни пуль
    public float destroyTime = 1f;

    // Флаг для определения, происходит ли зарядка боеприпасов
    private bool recharge;

    // Время зарядки боеприпасов
    public float rechargeTime = 2f;

    public void Awake()
    {
        Instance = this;

        // Инициализируем количество боеприпасов и максимальное количество
        AmmoCount = AmmoMemory;
        MaxAmmoCount = MaxAmmoMemory;

        // Обновляем отображение боеприпасов
        UpdateGun();
    }

    // Метод для сохранения состояния боеприпасов
    public float[] SaveAmmoCount()
    {
        return new float[] { AmmoCount, MaxAmmoCount };
    }

    // Метод для загрузки состояния боеприпасов
    public void LoadAmmoCount(float[] _AmmoCount)
    {
        AmmoCount = _AmmoCount[0];
        MaxAmmoCount = _AmmoCount[1];

        UpdateGun();
    }

    void Update()
    {
        // Если боеприпасов меньше 1 и не происходит зарядка и есть запасные боеприпасы
        if (AmmoCount < 1 && !recharge && MaxAmmoCount > 0)
        {
            // Начинаем зарядку
            recharge = true;
            Invoke(nameof(Recharge), rechargeTime);
        }
        else
        {
            // Уменьшаем время до следующего выстрела, если оно больше 0
            if (timeShot > 0)
            {
                timeShot -= Time.deltaTime;
            }
        }
    }

    // Метод для совершения выстрела, вызывается через кнопку в интерфесе игрока
    public void Fire()
    {
        // Если время до следующего выстрела прошло, есть боеприпасы и не идет зарядка
        if (timeShot <= 0 && AmmoCount > 0 && !recharge)
        {
            // Выпускаем пулю
            Spawn();

            // Устанавливаем время до следующего выстрела
            timeShot = startTime;

            // Уменьшаем количество боеприпасов
            AmmoCount -= 1;

            // Обновляем отображение боеприпасов
            UpdateGun();
        }
    }

    // Метод для перезарядки боеприпасов
    void Recharge()
    {
        // Устанавливаем количество боеприпасов в максимальное значение
        AmmoCount = AmmoMemory;

        // Уменьшаем максимальное количество боеприпасов
        MaxAmmoCount -= AmmoMemory;

        // Завершаем перезарядку
        recharge = false;

        // Обновляем отображение боеприпасов
        UpdateGun();
    }

    // Метод для создания пули
    void Spawn()
    {
        // Создаем экземпляр пули
        GameObject inst_obj = Instantiate(Bullet, ShotDir.position, ShotDir.rotation);

        // Устанавливаем время жизни и урон для пули
        Bullet bullet = inst_obj.GetComponent<Bullet>();
        bullet.destroyTime = destroyTime;
        bullet.damage = Damage;

        // Добавляем силу для движения пули
        Rigidbody2D rb = inst_obj.GetComponent<Rigidbody2D>();
        rb.AddForce(ShotDir.up * SpeedShoot, ForceMode2D.Impulse);
    }

    // Метод для обновления отображения боеприпасов
    private void UpdateGun()
    {
        AmmoMenu.text = $"{AmmoCount} / {MaxAmmoCount}";
    }
}
