using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] SpriteRenderer itemSprite;

    private void Start()
    {
        itemSprite.sprite = item.icon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Picked Up" + item.itemName);
            Inventory.Instance.AddPassiveItem((PassiveItem)item);
            Destroy(gameObject);
        }
    }
}
