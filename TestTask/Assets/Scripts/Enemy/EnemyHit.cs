using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthSistem player = other.GetComponentInParent<PlayerHealthSistem>();
            player.TakeDamage(damage);
        }
    }
}