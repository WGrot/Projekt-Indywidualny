using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ParriedProjectilesObjectPool : MonoBehaviour
{
    [SerializeField] ParriedProjectileBasic Prefab;
    public static ParriedProjectilesObjectPool Instance { get; private set; }

    private ObjectPool<ParriedProjectileBasic> projectilePool;
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

        projectilePool = new ObjectPool<ParriedProjectileBasic>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, 100, 1000);

    }

    private ParriedProjectileBasic CreatePooledObject()
    {
        ParriedProjectileBasic instance = Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        instance.OnDisableAction += ReturnObjectToPool;
        instance.gameObject.SetActive(false);

        return instance;
    }

    private void ReturnObjectToPool(ParriedProjectileBasic instance)
    {
        projectilePool.Release(instance);
    }

    private void OnTakeFromPool(ParriedProjectileBasic instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.SetParent(transform, true);
    }

    private void OnReturnToPool(ParriedProjectileBasic instance)
    {
        instance.gameObject.SetActive(false);
    }

    private void OnDestroyObject(ParriedProjectileBasic instance)
    {
        Destroy(instance.gameObject);
        instance.OnDisableAction -= ReturnObjectToPool;
    }


    public void ShootBullet(Vector3 position, Vector3 direction, float speed, float lifeTime, float damage)
    {
        ParriedProjectileBasic instance = projectilePool.Get();
        instance.Shoot(position, direction, speed, lifeTime, damage);
    }

}
