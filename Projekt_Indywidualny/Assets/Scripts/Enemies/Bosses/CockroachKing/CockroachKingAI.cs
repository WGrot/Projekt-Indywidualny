using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CockroachKingState
{
    Idle,
    Attack,
    Move
}

public class CockroachKingAI : EnemyHpBase
{
    private CockroachKingState currentState = CockroachKingState.Idle;

    [Header("Basic Configuration")]
    [SerializeField] private float idleTime = 1f;

    [Header("Jump Configuration")]
    [SerializeField] private GameObject landingDetectionPoint;
    [SerializeField] private GameObject shockwaveSpawnPoint;
    [SerializeField] private GameObject landingShockwave;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float landingDetectionRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxDistanceToPlayer;
    

    [Header("Summon Attack Configuration")]
    [SerializeField] private List<GameObject> SummonAttackEnemies;
    [SerializeField] private int minNumberOfSummons;

    [Header("Projectile Attack Configuration")]
    [SerializeField] private float projectileAttackTreshold;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private int projectileAmount = 5;
    [SerializeField] private float delayBetweenProjectiles = 0.1f;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifetime;
    [SerializeField] private float bulletSpread;
    [SerializeField] private float attackDamage;

    private float idleTimeLeft;
    private bool isLanding = false;

    private GameObject player;
    private Animator animator;
    private Rigidbody rb;
    private bool shockwaveSpawned;

    private float[] lastTimeAttackWasPerformed = { };

    public override void Start()
    {
        player = PlayerStatus.Instance.GetPlayerBody();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        idleTimeLeft = idleTime;
        base.currentHp = base.maxHp;
    }
    public void CheckIfLanding()
    {
        if (Physics.Raycast(landingDetectionPoint.transform.position, Vector3.down, landingDetectionRange, whatIsGround) && !isLanding)
        {
            shockwaveSpawned = false;
            isLanding = true;
            animator.SetBool("isLanding", true);
            animator.SetBool("isJumping", false);
            currentState = CockroachKingState.Idle;

        }

        

    }

    private void Update()
    {

        if (currentState == CockroachKingState.Idle)
        {
            idleTimeLeft -= Time.deltaTime;
        }

        if (idleTimeLeft < 0)
        {
            DetermineAttack();
        }

        if (rb.velocity.y < -6)
        {
            CheckIfLanding();
        }

        if (isLanding)
        {
            TryToSpawnShockwave();
        }

    }

    public void TryToSpawnShockwave()
    {
        if (Physics.Raycast(shockwaveSpawnPoint.transform.position, Vector3.down, 0.25f, whatIsGround) && !shockwaveSpawned)
        {
            shockwaveSpawned = true;
            landingShockwave.transform.position = shockwaveSpawnPoint.transform.position;
            landingShockwave.SetActive(true);
        }
    }

    public void Jump()
    {
        currentState = CockroachKingState.Move;
        isLanding = false;
        shockwaveSpawned = false;

        animator.SetBool("isLanding", false);
        animator.SetBool("isJumping", true);

        Vector3 jumpVector = new Vector3(0, 25, 0) + moveSpeed * (player.transform.position - transform.position);

        rb.velocity = jumpVector;


    }

    public void DetermineAttack()
    {
        int activeSummons = CountSpawnedEnemies();
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;

        currentState = CockroachKingState.Attack;

        if (activeSummons < minNumberOfSummons)
        {
            StartCoroutine(SummonAttack());
        }
        else
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                Jump();
            }
            else
            {
                StartCoroutine(ProjectileAttack());
            }

        }


        currentState = CockroachKingState.Idle;
        idleTimeLeft = idleTime - (1f * (maxHp - currentHp) / maxHp);

    }


    public IEnumerator SummonAttack()
    {
        animator.SetBool("isSummoning", true);
        foreach (GameObject enemy in SummonAttackEnemies)
        {
            enemy.SetActive(true);
        }
        yield return null;
        animator.SetBool("isSummoning", false);

    }

    public IEnumerator ProjectileAttack()
    {
        animator.SetBool("isSummoning", true);
        for (int i = 0; i < projectileAmount; i++)
        {
            Vector3 bulletDirection = player.transform.position + new Vector3(0, 0.25f, 0) - shootPoint.position;
            bulletDirection += new Vector3(
                                UnityEngine.Random.Range(-bulletSpread, bulletSpread),
                                UnityEngine.Random.Range(-bulletSpread, bulletSpread),
                                UnityEngine.Random.Range(-bulletSpread, bulletSpread)
                            );
            EnemyProjectileObjectPool.Instance.ShootBullet(shootPoint.position, bulletDirection, bulletSpeed, bulletLifetime, attackDamage);
            yield return new WaitForSeconds(delayBetweenProjectiles);
        }
        animator.SetBool("isSummoning", false);
    }

    private int CountSpawnedEnemies()
    {
        int result = 0;
        foreach (GameObject enemy in SummonAttackEnemies)
        {
            if (enemy.activeSelf)
            {
                result++;
            }
        }
        return result;
    }

}
