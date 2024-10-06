using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, I_Interactable, ILookable
{
    [SerializeField] public Item item;
    [SerializeField] protected SpriteRenderer itemSprite;
    [SerializeField] protected GameObject pickupIcon;
    [SerializeField] protected float pickupIconLifeTime;
    [SerializeField] protected ItemPools pool;

    protected bool isIconActive = false;
    
    public virtual void Start()
    {
        itemSprite.sprite = item.icon;
    }

    public void SetItem(Item item) { 
        this.item = item;
        itemSprite.sprite = item.icon;
    }

    public virtual void DoWhenLookedAt()
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
