using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    private Animator animator;
    public float speed = 10f;
    private GameObject player;

    public bool playerDetected;
    public float hitDistance=2f;

    private void Start()
    {
        animator=GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (playerDetected)
        {
            Move();
        }
    }

    public void Move()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if (Vector3.Distance(transform.position, player.transform.position) > hitDistance)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyHit")) 
            {
                animator.SetBool("EnemyWalk", true);
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                
            }
            
        }
        else
        {
            animator.SetBool("EnemyWalk", false);
            animator.SetTrigger("EnemyHit");
            
        }
        
    }
    

    public void Die()
    {
        Destroy(gameObject);
        EnemyController.Instance.Drop(transform);
    }
}
