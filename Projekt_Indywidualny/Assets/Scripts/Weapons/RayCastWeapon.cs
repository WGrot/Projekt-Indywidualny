using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastWeapon : WeaponBase
{
    [SerializeField] private float damage;
    [SerializeField] private float spread;
    [SerializeField] private int bulletsPerShoot;

    [SerializeField] private int maxAmmo;
    [SerializeField] private int clipSize;
    [SerializeField] private float reloadTime;


    [SerializeField] private bool HoldToShoot = false;

    private InputActions actions;

    private int ammoLeft;
    private int ammoInClip;
    public override void Shoot() { 
    
    
    }
    public override void Reload() { }
}
