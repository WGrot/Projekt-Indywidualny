using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CockroachAI : MonoBehaviour
{

    [Header( "CockroachMovement")]
    [SerializeField] private float speed;
    [SerializeField] private float lookRadius;
    [SerializeField] private float stopRadius;
    [SerializeField] private bool isAlwaysFollowing = true;

    [Header("AttackConfiguration")]
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;



    private bool isAttacking = false;
    private float distanceToPlayer;
    private GameObject player;
    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.stoppingDistance = stopRadius;
    }


    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer > attackRange && distanceToPlayer < lookRadius)
        {
            agent.SetDestination(player.transform.position);
        }
        else if(isAttacking != true)
        {
            StartCoroutine("AttackCoroutine");
        }



    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public float GetDistanceToPlayer()
    {
        return distanceToPlayer;
    }


    private IEnumerator AttackCoroutine()
    {
        isAttacking= true;
        agent.isStopped = true;
        yield return new WaitForSeconds(attackSpeed);
        Attack();
        agent.isStopped = false;
        isAttacking = false;
    }
    private void Attack()
    {
        if(distanceToPlayer <= attackRange)
        {
            PlayerStatus.Instance.TakeDamage(attackDamage);
        }
    }


}
