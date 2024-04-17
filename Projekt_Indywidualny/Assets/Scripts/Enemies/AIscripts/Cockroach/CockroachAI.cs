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

    [Header("AudioConfiguration")]
    [SerializeField] AudioClip walkingSound;
    [SerializeField] AudioClip attackSound;

    [Header("AnimationsConfiguration")]
    [SerializeField] AnimationClip WalkingAnimation;
    [SerializeField] AnimationClip IdleAnimation;


    private bool isAttacking = false;
    private float distanceToPlayer;
    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        agent.speed = speed;
        agent.stoppingDistance = stopRadius;
    }


    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer > attackRange && distanceToPlayer < lookRadius)
        {
            agent.SetDestination(player.transform.position);
            animator.SetBool("IsCrawling", true);

            if (!audioSource.isPlaying)
            {
                audioSource.clip = walkingSound;
                audioSource.Play();
            }
        }
        else if(isAttacking != true)
        {
            animator.SetBool("IsCrawling", false);
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
