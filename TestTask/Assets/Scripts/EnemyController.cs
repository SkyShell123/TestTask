using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;

    public List<GameObject> enemies;
    public int countEnemies;

    public DataBase data;

    public GameObject DropPref;
    private GameObject inst_obj;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < countEnemies; i++)
        {
            GameObject inst_obj = enemies[Random.Range(0, enemies.Count)];
            Vector2 position = new( Random.Range(-25, 25), Random.Range(-25, 25));
            Instantiate(inst_obj, position, transform.rotation);
        }
    }

    public void Drop(Transform transform)
    {
        inst_obj = Instantiate(DropPref, transform.position, transform.rotation);
        Drop drop = inst_obj.GetComponent<Drop>();
        Item item =  data.items[Random.Range(1, data.items.Count)];

        drop.item = item;
        drop.GetComponent<SpriteRenderer>().sprite = item.sprite;
    }
}
