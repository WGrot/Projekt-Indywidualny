using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DiggerAI : MonoBehaviour
{
    //[SerializeField] private GameObject projectile;
    [SerializeField] private float damage;
    [SerializeField] private float waitTimeAfterShot;
    [SerializeField] private float burrowTime;

    [SerializeField] private AudioClip burrowSound;
    [SerializeField] private AudioClip unBurrowSound;
    [SerializeField] private AudioClip attackSound;

    private AudioSource audioSource;

    private bool isPerformingAction = false;
    private Animator animator;
    private GameObject player;
    [SerializeField] private Transform shootPoint;
    public void Start()
    {
        player = PlayerStatus.Instance.GetPlayerBody();
        animator = GetComponent<Animator>();
        audioSource= GetComponent<AudioSource>();

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
        audioSource.clip = burrowSound;
        audioSource.Play();
        yield return new WaitForSeconds(burrowTime);
        Vector3 destination = GetRandomPositionInRoom(transform.position, 8f);
        transform.position = destination;
        animator.SetBool("isBurrowing", false);
        audioSource.clip = unBurrowSound;
        audioSource.Play();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);


        //Attacking
        animator.SetBool("isAttacking", true);
        audioSource.clip = attackSound;
        audioSource.Play();
        Vector3 bulletDirection = player.transform.position + new Vector3(0, 0.25f, 0) - shootPoint.position;
        EnemyProjectileObjectPool.Instance.ShootBullet(shootPoint.position, bulletDirection, 1, 10, 1);
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
            Debug.Log(hit.position.x + " " + hit.position.z);
            result = hit.position;
            return result;
        }
        result = startPos;
        return result;
    }
}
