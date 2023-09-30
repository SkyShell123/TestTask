using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    public EnemyAI enemyAI;
    public Transform RayDir;

    private void Start()
    {
        //enemyAI=GetComponentInParent<EnemyAI>();
    }

    private bool ReyDetector(string tag)
    {
        RaycastHit2D HitInfo = Physics2D.Raycast(transform.position, RayDir.transform.right);

        if (HitInfo.collider != null)
        {

            if (HitInfo.transform.CompareTag(tag))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!ReyDetector("Wall"))
            {
                enemyAI.PlayerDetected();
            }
        }
    }
}
