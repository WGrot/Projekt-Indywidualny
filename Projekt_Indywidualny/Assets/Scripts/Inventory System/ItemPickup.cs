using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, I_Interactable, ILookable
{
    [SerializeField] Item item;
    [SerializeField] SpriteRenderer itemSprite;
    [SerializeField] private GameObject pickupIcon;
    [SerializeField] private float pickupIconLifeTime;

    bool isIconActive = false;

    private void Start()
    {
        itemSprite.sprite = item.icon;
    }

    public void SetItem(Item item) { 
        this.item = item;
        itemSprite.sprite = item.icon;
    }

    public void DoWhenLookedAt()
    {
        if (!isIconActive)
        {
            StartCoroutine("ShowPickupIcon");
        }

    }

    public void InteractWithPlayer()
    {
        Debug.Log("Picked Up" + item.itemName);
        if (item is PassiveItem)
        {
            Inventory.Instance.AddPassiveItem((PassiveItem)item);
        }
        else if (item is WeaponSO)
        {
            Inventory.Instance.AddWeapon((WeaponSO)item);
        }

        Destroy(gameObject);
    }

    IEnumerator ShowPickupIcon()
    {
        pickupIcon.SetActive(true);
        isIconActive= true;
        yield return new WaitForSeconds(pickupIconLifeTime);
        pickupIcon.SetActive(false);
        isIconActive= false;
    }
}
