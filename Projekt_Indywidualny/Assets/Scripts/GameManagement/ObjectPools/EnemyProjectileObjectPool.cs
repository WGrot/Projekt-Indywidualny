using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyProjectileObjectPool : MonoBehaviour
{
    [SerializeField] EnemyProjectileBasic Prefab;

    [SerializeField] int amountCreatedOnSpawn = 20;
    public static EnemyProjectileObjectPool Instance { get; private set; }

    private ObjectPool<EnemyProjectileBasic> projectilePool;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one EnemyProjectileObjectPool instance found!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        projectilePool = new ObjectPool<EnemyProjectileBasic>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, 100, 1000);

    }

    private EnemyProjectileBasic CreatePooledObject()
    {
        EnemyProjectileBasic instance = Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        instance.OnDisableAction += ReturnObjectToPool;
        instance.gameObject.SetActive(false);

        return instance;
    }

    private void ReturnObjectToPool(EnemyProjectileBasic instance)
    {
        projectilePool.Release(instance);
    }

    private void OnTakeFromPool(EnemyProjectileBasic instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.SetParent(transform, true);
    }

    private void OnReturnToPool(EnemyProjectileBasic instance)
    {
        instance.gameObject.SetActive(false);
    }

    private void OnDestroyObject(EnemyProjectileBasic instance)
    {
        Destroy(instance.gameObject);
        instance.OnDisableAction -= ReturnObjectToPool;
    }


    public void ShootBullet(Vector3 position, Vector3 direction, float speed, float lifeTime, float damage)
    {
        EnemyProjectileBasic instance = projectilePool.Get();
        instance.Shoot(position, direction, speed, lifeTime, damage);
    }


}
