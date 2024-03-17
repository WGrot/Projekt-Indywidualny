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
    private List<WeaponSO> weapons;
    private InputActions inputActions;


    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }
    public void OnEnable()
    {
        inputActions.Player_base.Scroll.performed += SwitchWeapon;
        inputActions.Player_base.Shoot.performed += TryToShoot;
        Inventory.OnWeaponChangedCallback += EquipNewWeapon;
    }
    public void OnDisable()
    {
        inputActions.Player_base.Scroll.performed -= SwitchWeapon;
        inputActions.Player_base.Shoot.performed -= TryToShoot;
        Inventory.OnWeaponChangedCallback -= EquipNewWeapon;
    }

    public IEnumerator Start()
    {
        yield return null;
        weapons = Inventory.Instance.GetWeaponsList();
        weaponSprite = weaponObject.GetComponent<SpriteRenderer>();

    }

    #region SwitchingWeapons

    public void SwitchWeapon(InputAction.CallbackContext context)
    {
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
        weaponObject.transform.position = transform.position + activeWeapon.SpawnPoint;
        weaponObject.transform.rotation = transform.rotation * Quaternion.Euler(activeWeapon.SpawnRotation);
        weaponSprite.sprite = activeWeapon.icon;
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
        weaponObject.transform.position = transform.position + activeWeapon.SpawnPoint;
        weaponObject.transform.rotation = transform.rotation * Quaternion.Euler(activeWeapon.SpawnRotation);
        weaponSprite.sprite = activeWeapon.icon;
    }

    private void EquipNewWeapon()
    {
        activeWeaponID = weapons.Count - 1;
        activeWeapon = weapons[activeWeaponID];
        weaponObject.transform.position = transform.position + activeWeapon.SpawnPoint;
        weaponObject.transform.rotation = transform.rotation * Quaternion.Euler(activeWeapon.SpawnRotation);
        weaponSprite.sprite = activeWeapon.icon;
    }
    #endregion

    public void TryToShoot(InputAction.CallbackContext context)
    {
        if (activeWeapon == null)
        {
            return;
        }
        activeWeapon.Shoot(gameObject);
    }
}
