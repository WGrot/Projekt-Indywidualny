using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DiggerAI : MonoBehaviour
{
    [Header("Basic Configuration")]
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float waitTimeAfterShot;
    [SerializeField] private float burrowTime;
    [SerializeField] private float timeOffset = 0;


    [Header("Audio Configuration")]
    [SerializeField] private AudioClip burrowSound;
    [SerializeField] private AudioClip unBurrowSound;
    [SerializeField] private AudioClip attackSound;

    [Header("References")]
    [SerializeField] private ParticleSystem burrowParticles;
    [SerializeField] private Transform shootPoint;

    private AudioSource audioSource;
    private bool isPerformingAction = true;
    private Animator animator;
    private GameObject player;

    public void Start()
    {
        player = PlayerStatus.Instance.GetPlayerBody();
        animator = GetComponent<Animator>();
        audioSource= GetComponent<AudioSource>();
        Invoke("ApplyOffset", timeOffset);
    }

    void ApplyOffset()
    {
        isPerformingAction = false;
    }

    private void Update()
    {
        if (!isPerformingAction)
        {
            isPerformingAction = true;
            StartCoroutine(PerformAction());
        }
    }

    IEnumerator PerformAction()
    {
        //Burrowing
        animator.SetBool("isBurrowing", true);
        burrowParticles.Play();
        audioSource.clip = burrowSound;
        audioSource.Play();
        yield return new WaitForSeconds(burrowTime);
        Vector3 destination = GetRandomPositionInRoom(transform.position, 8f);
        transform.position = destination;
        animator.SetBool("isBurrowing", false);
        burrowParticles.Play();
        audioSource.clip = unBurrowSound;
        audioSource.Play();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);


        //Attacking
        animator.SetBool("isAttacking", true);
        audioSource.clip = attackSound;
        audioSource.Play();
        Vector3 bulletDirection = player.transform.position + new Vector3(0, 0.25f, 0) - shootPoint.position;
        EnemyProjectileObjectPool.Instance.ShootBullet(shootPoint.position, bulletDirection, bulletSpeed, bulletLifeTime, bulletDamage);
        yield return null;
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(waitTimeAfterShot);
        isPerformingAction = false;
    }


    private Vector3 GetRandomPositionInRoom(Vector3 startPos, float range)
    {
        Vector3 randomPoint = startPos + Random.insideUnitSphere * range;
        randomPoint.y = transform.position.y;
        Vector3 result;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas)) 
        {

            result = hit.position;
            return result;
        }
        result = startPos;
        return result;
    }
}
