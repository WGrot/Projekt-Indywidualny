using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapons/Weapon")]
public class WeaponSO : Item
{
    public GameObject ModelPrefab;
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;
    public Vector3 ShootPoint;
    public ShootConfigurationSO ShootConfig;



    private MonoBehaviour activeMonobehaviour;
    private GameObject model;
    private float lastShootTime;
    private GameObject shootSystem;
    private ObjectPool<TrailRenderer> trailPool;

    public void OnPickup()
    {
        Debug.Log("podniesiono cuœ");
     }

    public void Shoot(GameObject weaponHolder)
    {
        if(Physics.Raycast(weaponHolder.transform.position + ShootPoint, weaponHolder.transform.forward, out RaycastHit hit))
        {
            Instantiate(ModelPrefab, hit.point, Quaternion.identity);
        }
    }


}
