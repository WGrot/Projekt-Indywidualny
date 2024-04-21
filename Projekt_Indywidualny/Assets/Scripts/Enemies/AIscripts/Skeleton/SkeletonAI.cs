using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class SkeletonAI : MonoBehaviour
{
    private enum SkeletonState {Following, Attacking}

    [Header("CockroachMovement")]
    [SerializeField] private float speed;
    [SerializeField] private float lookRadius;
    [SerializeField] private float stopRadius;

    [Header("AttackConfiguration")]
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifetime;
    [SerializeField] private float bulletSpread;
    [SerializeField] private int bulletsPerShoot;

    [SerializeField] private Transform shootPoint;

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
            animator.SetBool("isAtacking", false);
            animator.speed = 1;
            agent.speed = speed;
            agent.SetDestination(player.transform.position);


        }else if (distanceToPlayer < attackRange)
        {
            if (isAttacking != true)
            {
                StartCoroutine("AttackCoroutine");
            }
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
        isAttacking = true;
        agent.isStopped = true;
        animator.SetBool("isAtacking", true);
        animator.speed = 1 / attackSpeed;
        Attack();
        yield return new WaitForSeconds(attackSpeed);
        agent.isStopped = false;
        isAttacking = false;
    }
    private void Attack()
    {
        Vector3 bulletDirection = player.transform.position + new Vector3(0, 0.25f, 0) - shootPoint.position;
        for (int i = 0; i < bulletsPerShoot; i++)
        {
            bulletDirection += new Vector3(
                                UnityEngine.Random.Range(-bulletSpread, bulletSpread),
                                UnityEngine.Random.Range(-bulletSpread, bulletSpread),
                                UnityEngine.Random.Range(-bulletSpread, bulletSpread)
                            );
            EnemyProjectileObjectPool.Instance.ShootBullet(shootPoint.position, bulletDirection, bulletSpeed, bulletLifetime, attackDamage);
        }
        audioSource.clip = attackSound;
        audioSource.Play();
    }


}
