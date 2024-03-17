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

    public ShootConfigurationSO ShootConfig;
    //public TrailConfigurationSO TrailConfig;


    private MonoBehaviour activeMonobehaviour;
    private GameObject model;
    private float lastShootTime;
    private GameObject shootSystem;
    private ObjectPool<TrailRenderer> trailPool;

    public void OnPickup()
    {

        Debug.Log("podniesiono cuœ");
        /*
        this.activeMonobehaviour = activeMonobehaviour;
        lastShootTime = 0;
        model = Instantiate(ModelPrefab);
        model.transform.SetParent(parent, false);
        model.transform.position = SpawnPoint;
        model.transform.rotation = Quaternion.Euler(SpawnRotation);
        */
     }



}
