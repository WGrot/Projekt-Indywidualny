using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, I_Interactable, ILookable
{
    [SerializeField] public Item item;
    [SerializeField] protected SpriteRenderer itemSprite;
    [SerializeField] private GameObject pickupIcon;
    [SerializeField] private float pickupIconLifeTime;

    bool isIconActive = false;

    public virtual void Start()
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

    public virtual void InteractWithPlayer()
    {
        if (item.itemBehaviour != null)
        {
            item.itemBehaviour.OnPickup();
        }
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
