using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFind : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Loot"))
        {
            Drop drop = other.GetComponent<Drop>();
            if (Inventory.Instance.TakeItem(drop.item)) 
            {
                Destroy(other.gameObject);
            }
        }
    }
}
