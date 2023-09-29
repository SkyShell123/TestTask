using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    internal float destroyTime;
    internal int damage;

    private void Start()
    {
        Invoke(nameof(DestroyBullet), destroyTime);
    }

   
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            HealthSistem enemy = other.GetComponentInParent<HealthSistem>();
            //EnemyDetect enemyDet = other.transform...GetComponentInParent<EnemyDetect>();
            enemy.TakeDamage(damage);
            //enemyDet.PlayerTrigger(true);
            DestroyBullet();
        }
        else if (other.CompareTag("Wall"))
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
