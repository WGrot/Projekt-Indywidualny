using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CockroachKingState
{
    Idle,
    Attack,
    Move
}

public class CockroachKingAI : BossHpBase
{
    private CockroachKingState currentState = CockroachKingState.Idle;

    [Header("Basic Configuration")]
    [SerializeField] private float idleTime = 1f;
    [SerializeField] AudioClip bossTheme;
    [SerializeField] Sprite bossIcon;

    [Header("Jump Configuration")]
    [SerializeField] private GameObject landingDetectionPoint;
    [SerializeField] private GameObject shockwaveSpawnPoint;
    [SerializeField] private GameObject landingShockwave;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float landingDetectionRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxDistanceToPlayer;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip landingSound;


    [Header("Summon Attack Configuration")]
    [SerializeField] private List<GameObject> SummonAttackEnemies;
    [SerializeField] private int minNumberOfSummons;
    [SerializeField] AudioClip summonAttackSound;

    [Header("Projectile Attack Configuration")]
    [SerializeField] private float projectileAttackTreshold;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private int projectileAmount = 5;
    [SerializeField] private float delayBetweenProjectiles = 0.1f;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifetime;
    [SerializeField] private float bulletSpread;
    [SerializeField] private float attackDamage;
    [SerializeField] AudioClip projectileSound;

    private float idleTimeLeft;
    private bool isLanding = false;

    private GameObject player;
    private Animator animator;
    private Rigidbody rb;
    private bool shockwaveSpawned;
    private AudioSource audioSource;

    public override void Start()
    {
        player = PlayerStatus.Instance.GetPlayerBody();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        idleTimeLeft = idleTime;
        base.currentHp = base.maxHp;
        MusicManager.Instance.PlaySpecialMusic(bossTheme);
        base.CallOnBossFightStarted(bossIcon);
    }

    private void OnDisable()
    {
        MusicManager.Instance.PlayDefaultMusic();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        CallOnBossHpChanged(currentHp, maxHp);
    }

    public override void Die()
    {
        CallOnBossFightEnded();
        base.Die();

    }

    public override void Heal(float healAmount)
    {
        base.Heal(healAmount);
        CallOnBossHpChanged(currentHp, maxHp);
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
            audioSource.clip = landingSound;
            audioSource.Play();
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
        audioSource.clip = jumpSound;
        audioSource.Play();


    }

    public void DetermineAttack()
    {
        int activeSummons = CountSpawnedEnemies();

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
        audioSource.clip = summonAttackSound; 
        audioSource.Play();
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
        audioSource.clip = projectileSound;
        for (int i = 0; i < projectileAmount; i++)
        {
            Vector3 bulletDirection = player.transform.position + new Vector3(0, 0.25f, 0) - shootPoint.position;
            bulletDirection += new Vector3(
                                UnityEngine.Random.Range(-bulletSpread, bulletSpread),
                                UnityEngine.Random.Range(-bulletSpread, bulletSpread),
                                UnityEngine.Random.Range(-bulletSpread, bulletSpread)
                            );
            EnemyProjectileObjectPool.Instance.ShootBullet(shootPoint.position, bulletDirection, bulletSpeed, bulletLifetime, attackDamage);
            audioSource.Play();
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
