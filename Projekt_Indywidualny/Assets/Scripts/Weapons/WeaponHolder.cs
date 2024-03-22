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


    private List<WeaponSO> weapons;
    private List<WeaponPrefix> prefixes;
    private InputActions inputActions;

    private bool wasShootPressedThisFrame = false;
    private float chargeTime = 0f;


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
        Inventory.OnWeaponAddedCallback += EquipNewWeapon;
    }
    public void OnDisable()
    {
        inputActions.Player_base.Scroll.performed -= SwitchWeapon;
        inputActions.Player_base.Shoot.canceled -= TryToShootCharge;
        inputActions.Player_base.DropWeapon.performed -= DropWeapon;
        Inventory.OnWeaponAddedCallback -= EquipNewWeapon;
    }

    public IEnumerator Start()
    {
        yield return null;
        weapons = Inventory.Instance.GetWeaponsList();
        prefixes = Inventory.Instance.GetPrefixList();
        weaponSprite = weaponObject.GetComponent<SpriteRenderer>();

    }

    #region SwitchingWeapons

    public void SwitchWeapon(InputAction.CallbackContext context)
    {
        if (weapons.Count < 1)
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
        activeWeapon = weapons[activeWeaponID];
        weapons[activeWeaponID].ApplyPrefix(prefixes[activeWeaponID]);
        SetWeaponModel();
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
        activeWeapon = weapons[activeWeaponID];
        weapons[activeWeaponID].ApplyPrefix(prefixes[activeWeaponID]);
        SetWeaponModel();
    }

    private void SetWeaponModel()
    {
        weaponObject.transform.localPosition = transform.localPosition + activeWeapon.SpawnPoint;
        Debug.Log(transform.localPosition + "activeweapon sp: " + activeWeapon.SpawnPoint);
        weaponObject.transform.rotation = transform.rotation * Quaternion.Euler(activeWeapon.SpawnRotation);
        weaponObject.transform.localScale = 1 * activeWeapon.ModelScale;
        weaponSprite.sprite = activeWeapon.icon;
    }

    private void EquipNewWeapon()
    {
        activeWeaponID = weapons.Count - 1;
        activeWeapon = weapons[activeWeaponID];
        activeWeapon.OnEquip();
        Debug.Log(activeWeaponID);
        //activeWeapon.ApplyPrefix(prefixes[activeWeaponID]);
        activeWeapon = weapons[activeWeaponID];
        SetWeaponModel();
    }
    #endregion

    private void Update()
    {
        if (inputActions.Player_base.Shoot.IsPressed())
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

        if (activeWeapon.shootStyle == WeaponShootingStyle.FullAuto)
        {
            activeWeapon.Shoot(gameObject);
        }
        else if (activeWeapon.shootStyle == WeaponShootingStyle.OneTap)
        {
            if (wasShootPressedThisFrame)
            {
                return;
            }
            activeWeapon.Shoot(gameObject);
        }


    }

    public void TryToShootCharge(InputAction.CallbackContext context)
    {
        if (activeWeapon == null)
        {
            return;
        }

        if (activeWeapon.shootStyle == WeaponShootingStyle.Charge && activeWeapon.chargeTime < chargeTime)
        {
            activeWeapon.Shoot(gameObject);
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
        pickupInstance.GetComponent<WeaponPickup>().SetWeaponAndPrefix(activeWeapon, prefixes[activeWeaponID]);
        activeWeapon = null;
        Inventory.Instance.RemovePrefix(prefixes[activeWeaponID]);
        if (weapons.Count > 0)
        {
            SwitchToPreviousWeapon();
        }
        else
        {
            Disarm();
        }


        

    }

    private void Disarm()
    {
        activeWeapon = null;
        activeWeaponID = 0;
        weaponSprite.sprite = null;
    }

}
