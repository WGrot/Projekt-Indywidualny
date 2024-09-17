using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DiggerAI : EnemyHpBase
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float damage;
    [SerializeField] private float waitTimeAfterShot;
    [SerializeField] private float burrowTime;


    private GameObject player;
    [SerializeField] private Transform shootPoint;
    public override void Start()
    {
        base.Start();
        player = PlayerStatus.Instance.GetPlayerBody();
        StartCoroutine(Teleport());
        StartCoroutine(Attack());

    }

    IEnumerator Attack()
    {
        Vector3 bulletDirection = player.transform.position + new Vector3(0, 0.25f, 0) - shootPoint.position;

        EnemyProjectileObjectPool.Instance.ShootBullet(shootPoint.position, bulletDirection, 1, 10, 1);
        yield return null;
    }

    IEnumerator Teleport()
    {

            Vector3 Destination = GetRandomPositionInRoom(transform.position, 8f);
            transform.position = Destination;
            Debug.Log("teleported " + Destination.x + " " + Destination.y + " " + Destination.z);
            yield return new WaitForSeconds(2f);

    }

    private Vector3 GetRandomPositionInRoom(Vector3 startPos, float range)
    {
        Vector3 randomPoint = startPos + Random.insideUnitSphere * range;
        randomPoint.y = transform.position.y;
        Vector3 result;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas)) 
        {
            Debug.Log("chuj");
            result = hit.position;
            return result;
        }
        result = startPos;
        return result;
    }
}
