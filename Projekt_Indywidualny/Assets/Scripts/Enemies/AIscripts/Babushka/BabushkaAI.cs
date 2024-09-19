using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BabushkaAI : MonoBehaviour
{
    [Header("CockroachMovement")]
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;


    [Header("AttackConfiguration")]
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private GameObject attackPoint;

    [Header("AudioConfiguration")]
    [SerializeField] AudioClip walkingSound;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip deathSound;



    private bool isAttacking = false;
    private float distanceToPlayer;
    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;

    public void Start()
    {
        player = PlayerStatus.Instance.GetPlayerBody();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        agent.speed = speed;

    }


    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer > attackRange)
        {
            agent.SetDestination(player.transform.position);
            animator.SetBool("isWalking", true);
            if (!audioSource.isPlaying)
            {
                audioSource.clip = walkingSound;
                audioSource.Play();
            }
        }
        else if (isAttacking != true)
        {
            StartCoroutine("AttackCoroutine");
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


    private IEnumerator AttackCoroutine()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);
        isAttacking = true;
        agent.isStopped = true;
        audioSource.Stop();
        audioSource.clip = attackSound;
        audioSource.Play();

        yield return new WaitForSeconds(attackSpeed);

        agent.isStopped = false;
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        yield return null;
    }
}
