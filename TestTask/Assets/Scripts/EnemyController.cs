using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;

    // Список врагов и количество врагов для генерации при старте.
    public List<GameObject> enemies;
    public int countEnemies;

    // Ссылка на экземпляр класса DataBase для доступа к данным.
    public DataBase data;

    // Префаб для выпадающего предмета.
    public GameObject DropPref;
    private GameObject inst_obj; // Временная переменная для хранения созданного предмета.

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Генерируем врагов при старте игры.
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        // Генерируем случайное количество врагов из списка и располагаем их на случайных позициях.
        for (int i = 0; i < countEnemies; i++)
        {
            GameObject inst_obj = enemies[Random.Range(0, enemies.Count)];
            Vector2 position = new(Random.Range(-25, 25), Random.Range(-25, 25));
            Instantiate(inst_obj, position, transform.rotation);
        }
    }

    public void Drop(Transform transform)
    {
        // Создаем выпадающий предмет и устанавливаем его параметры (предмет и изображение) случайно из данных.
        inst_obj = Instantiate(DropPref, transform.position, transform.rotation);
        Drop drop = inst_obj.GetComponent<Drop>();
        Item item = data.items[Random.Range(1, data.items.Count)];

        drop.item = item;
        drop.GetComponent<SpriteRenderer>().sprite = item.sprite;
    }
}
