using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    EnemyAI enemyAI;
    public Transform RayDir;
    //private GameObject player;

    public bool chill = true;
    public bool search = false;
    public bool agro = false;

    private bool searchill;
    private Transform target;

    private void Start()
    {
        enemyAI=GetComponentInParent<EnemyAI>();
    }

    private void Update()
    {

        if (chill)
        {
            Chill();
        }
        else if (search)
        {
            Search();
        }
        else if (agro)
        {
            Agro();
        }
    }

    public void PlayerTrigger(bool state)
    {
        chill = false;
        search = !state;
        agro = state;
    }

    public bool IsSearch()
    {
        if (search)
        {
            return true;
        }
        return false;
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
    private void SearchToChill()
    {
        search = false;
        agro = false;
        chill = true;
        searchill = false;
    }
    private void Chill()
    {
        enemyAI.playerDetected = false;
        
    }
    private void Search()
    {
        if (!searchill)
        {
            searchill = true;
            Invoke(nameof(SearchToChill), 10f);
        }

        if (ReyDetector("Player"))
        {
            PlayerTrigger(true);
            CancelInvoke(nameof(SearchToChill));
            searchill = false;
        }

        

    }

    private void Agro()
    {

        enemyAI.playerDetected = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!ReyDetector("Wall"))
            {
                PlayerTrigger(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTrigger(false);
        }
    }
}
