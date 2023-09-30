using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    private Animator animator;
    public float speed = 10f;
    private GameObject player;
    public bool isAttack;

    private bool playerDetected;
    public float hitDistance=2f;

    private void Start()
    {
        animator=GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlayerDetected()
    {
        playerDetected=true;
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
            if (!isAttack)
            {
                animator.SetBool("IsAttacking", false); // Убедитесь, что атака выключена
                animator.SetBool("EnemyWalk", true);
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            
        }
        else
        {
            if (!isAttack)
            {
                animator.SetBool("EnemyWalk", false);
                animator.SetBool("IsAttacking", true); // Установите атаку включенной
            }
            
            
            //animator.SetTrigger("EnemyHit");
        }

    }
    

    public void Die()
    {
        Destroy(gameObject);
        EnemyController.Instance.Drop(transform);
    }
}
