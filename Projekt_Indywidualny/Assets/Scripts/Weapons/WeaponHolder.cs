using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    private int activeWeaponID = 0;
    private WeaponSO activeWeapon;
    [SerializeField] private GameObject weaponObject;
    private SpriteRenderer weaponSprite;
    [SerializeField] private GameObject weaponPickup;

    [SerializeField] private AudioClip changeWeaponSound;

    private List<WeaponSO> weapons;
    private List<WeaponPrefix> prefixes;
    private List<AmmoData> ammos;
    private InputActions inputActions;

    private bool wasShootPressedThisFrame = false;
    private bool isReloading = false;
    private float chargeTime = 0f;
    private AudioSource weaponAudioSource;
    private AudioSource selfAudioSource;


    public delegate void OnWeaponChanged(int activeWeaponId);
    public static event OnWeaponChanged OnWeaponChangedCallback;
    public delegate void OnWeaponShoot(int activeWeaponId);
    public static event OnWeaponShoot OnWeaponShootCallback;
    public delegate void OnWeaponReload(int activeWeaponId);
    public static event OnWeaponReload OnWeaponReloadCallback;


    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }
    public void OnEnable()
    {
        inputActions.Player_base.Scroll.performed += SwitchWeapon;
        inputActions.Player_base.Shoot.canceled += TryToShootCharge;
        inputActions.Player_base.DropWeapon.performed += DropWeapon;
        inputActions.Player_base.Reload.performed += OnReloadCallback;
        Inventory.OnWeaponAddedCallback += EquipNewWeapon;
        


    }
    public void OnDisable()
    {
        inputActions.Player_base.Scroll.performed -= SwitchWeapon;
        inputActions.Player_base.Shoot.canceled -= TryToShootCharge;
        inputActions.Player_base.DropWeapon.performed -= DropWeapon;
        inputActions.Player_base.Reload.performed -= OnReloadCallback;
        Inventory.OnWeaponAddedCallback -= EquipNewWeapon;
    }

    public IEnumerator Start()
    {
        yield return null;
        weapons = Inventory.Instance.GetWeaponsList();
        prefixes = Inventory.Instance.GetPrefixList();
        ammos = Inventory.Instance.GetAmmoList();
        
        weaponSprite = weaponObject.GetComponent<SpriteRenderer>();
        weaponAudioSource = weaponObject.GetComponent<AudioSource>();
        selfAudioSource = GetComponent<AudioSource>();

        if (weapons.Count > 0) //Dziêki tej linijce gracz nie spawnuje siê ze schowan¹ broni¹
        {
            OnStartSetupWeapon();
        }

    }

    #region SwitchingWeapons

    public void SwitchWeapon(InputAction.CallbackContext context)
    {
        if (weapons.Count < 2 || GameStateManager.Instance.isGamePaused())
        {
            return;
        }
        if (context.ReadValueAsButton())
        {
            SwitchToNextWeapon();
        }
        else
        {
            SwitchToPreviousWeapon();
        }

        selfAudioSource.clip = changeWeaponSound;
        selfAudioSource.Play();

    }

    public void SwitchToNextWeapon()
    {
        if (activeWeaponID + 1 > weapons.Count - 1)
        {
            activeWeaponID = 0;

        }
        else
        {
            activeWeaponID += 1;
        }
        Inventory.Instance.SetActiveWeaponID(activeWeaponID);
        activeWeapon = weapons[activeWeaponID];
        weapons[activeWeaponID].ApplyPrefix(prefixes[activeWeaponID]);
        SetWeaponModel();
        if (OnWeaponChangedCallback != null)
        {
            OnWeaponChangedCallback(activeWeaponID);
        }
    }

    public void SwitchToPreviousWeapon()
    {
        if (activeWeaponID - 1 < 0)
        {
            activeWeaponID = weapons.Count - 1;

        }
        else
        {
            activeWeaponID -= 1;
        }
        Inventory.Instance.SetActiveWeaponID(activeWeaponID);
        activeWeapon = weapons[activeWeaponID];
        weapons[activeWeaponID].ApplyPrefix(prefixes[activeWeaponID]);
        SetWeaponModel();
        if (OnWeaponChangedCallback != null)
        {
            OnWeaponChangedCallback(activeWeaponID);
        }
    }

    private void OnStartSetupWeapon()
    {
        Inventory.Instance.SetActiveWeaponID(activeWeaponID);
        activeWeapon = weapons[activeWeaponID];
        weapons[activeWeaponID].ApplyPrefix(prefixes[activeWeaponID]);
        SetWeaponModel();
    }

    private void SetWeaponModel()
    {
        weaponObject.transform.localPosition = transform.localPosition + activeWeapon.SpawnPoint;
        weaponObject.transform.rotation = transform.rotation * Quaternion.Euler(activeWeapon.SpawnRotation);
        weaponObject.transform.localScale = 1 * activeWeapon.ModelScale;
        weaponSprite.sprite = activeWeapon.icon;
    }

    private void EquipNewWeapon()
    {
        activeWeaponID = weapons.Count - 1;
        Inventory.Instance.SetActiveWeaponID(activeWeaponID);
        activeWeapon = weapons[activeWeaponID];
        activeWeapon.OnEquip();
        activeWeapon.ApplyPrefix(prefixes[activeWeaponID]);
        activeWeapon = weapons[activeWeaponID];
        SetWeaponModel();
        if (OnWeaponChangedCallback != null)
        {
            OnWeaponChangedCallback(activeWeaponID);
        }
        selfAudioSource.clip = changeWeaponSound;
        selfAudioSource.Play();
    }
    #endregion

    private void Update()
    {
        if (inputActions.Player_base.Shoot.IsPressed() && !isReloading)
        {
            TryToShootClassic();
            wasShootPressedThisFrame = true;
            chargeTime += Time.deltaTime;
        }
        else
        {
            wasShootPressedThisFrame = false;
            chargeTime = 0;
        }
    }

    public void TryToShootClassic()
    {
        if (activeWeapon == null)
        {
            return;
        }

        if (ammos[activeWeaponID].ammoLeft < 1)
        {
            Debug.Log("koniec amunicji!! ~Bogdan");
            return;
        }

        if (GameStateManager.Instance.isGamePaused())
        {
            return;
        }

        if (ammos[activeWeaponID].ammoInClip <1 && ammos[activeWeaponID].ammoLeft > 0)
        {
            if (!isReloading)
            {
                StartReloadCoroutine();
            }
            return;
        }

        if (activeWeapon.shootStyle == WeaponShootingStyle.FullAuto)
        {
            bool shooted = activeWeapon.Shoot(gameObject, activeWeaponID);
            if (!shooted)
            {
                return;
            }

        }
        else if (activeWeapon.shootStyle == WeaponShootingStyle.OneTap)
        {
            if (wasShootPressedThisFrame)
            {
                return;
            }

            bool shooted = activeWeapon.Shoot(gameObject, activeWeaponID);
            if (!shooted)
            {
                return;
            }

        }

        weaponAudioSource.clip = activeWeapon.weaponShootSound;
        weaponAudioSource.Play();
        if (OnWeaponShootCallback != null)
        {
            OnWeaponShootCallback(activeWeaponID);
        }


    }

    public void TryToShootCharge(InputAction.CallbackContext context)
    {
        if (activeWeapon == null)
        {
            return;
        }

        if (ammos[activeWeaponID].ammoLeft < 1)
        {
            Debug.Log("koniec amunicji!! ~Bogdan");
            return;
        }

        if (GameStateManager.Instance.isGamePaused())
        {
            return;
        }

        if (activeWeapon.shootStyle == WeaponShootingStyle.Charge && activeWeapon.chargeTime < chargeTime)
        {
            bool shooted = activeWeapon.Shoot(gameObject, activeWeaponID);
            if (!shooted)
            {
                return;
            }
            weaponAudioSource.clip = activeWeapon.weaponShootSound;
            weaponAudioSource.Play();
            if (OnWeaponShootCallback != null)
            {
                OnWeaponShootCallback(activeWeaponID);
            }
        }

    }

    public void DropWeapon(InputAction.CallbackContext context)
    {
        if (weapons.Count == 0)
        {
            return;
        }

        Inventory.Instance.RemoveWeapon(activeWeapon);
        GameObject pickupInstance = Instantiate(weaponPickup, transform.position, Quaternion.identity);
        pickupInstance.GetComponent<WeaponPickup>().SetWeaponAndPrefixAndAmmo(activeWeapon, prefixes[activeWeaponID], ammos[activeWeaponID]);
        activeWeapon = null;
        Inventory.Instance.RemovePrefix(prefixes[activeWeaponID]);
        Inventory.Instance.RemoveAmmo(ammos[activeWeaponID]);
        if (weapons.Count > 0)
        {
            SwitchToPreviousWeapon();
        }
        else
        {
            Disarm();
        }

    }

    public void DropWeaponAtIndex(int index)
    {
        if (weapons.Count == 0)
        {
            return;
        }

        if (index > weapons.Count -1)
        {
            Debug.LogError("You tried to delete weapon at index that excedes number of weapons");
            return;
        }


        GameObject pickupInstance = Instantiate(weaponPickup, transform.position, Quaternion.identity);
        pickupInstance.GetComponent<WeaponPickup>().SetWeaponAndPrefixAndAmmo(weapons[index], prefixes[index], ammos[index]);
        Inventory.Instance.RemoveWeaponAtIndex(index);
        Inventory.Instance.RemovePrefixAtIndex(index);
        Inventory.Instance.RemoveAmmoAtIndex(index);
        if (activeWeaponID == index)
        {
            activeWeapon = null;
        }
        if (weapons.Count > 0)
        {
            SwitchToPreviousWeapon();
        }
        else
        {
            Disarm();
        }

    }

    public void OnReloadCallback(InputAction.CallbackContext context)
    {
        StartReloadCoroutine();
    }

    public void StartReloadCoroutine()
    {
        if (ammos[activeWeaponID].ammoInClip == activeWeapon.ClipSize)
        {
            return;
        }
        IEnumerator coroutine = ReloadCoroutine();
        StartCoroutine(coroutine);
    }
    public IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        selfAudioSource.clip = activeWeapon.weaponReloadSound;
        selfAudioSource.Play();

        yield return new WaitForSeconds(activeWeapon.reloadTime / PlayerStatus.Instance.GetCharacterStatValueOfType(StatType.ReloadSpeed));
        Inventory.Instance.GetAmmoAtIndex(activeWeaponID).ReloadClip(activeWeapon.ClipSize);
        isReloading= false;

        if (OnWeaponReloadCallback != null)
        {
            OnWeaponReloadCallback(activeWeaponID);
        }
    }
    private void Disarm()
    {
        activeWeapon = null;
        activeWeaponID = 0;
        Inventory.Instance.SetActiveWeaponID(activeWeaponID);
        weaponSprite.sprite = null;
    }

}
