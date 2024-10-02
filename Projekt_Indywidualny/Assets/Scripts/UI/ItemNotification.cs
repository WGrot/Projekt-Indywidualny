using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemNotification : MonoBehaviour
{
    [SerializeField] private GameObject itemNotificationBody;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI shortDescription;
    [SerializeField] private TextMeshProUGUI unlockedText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject brokenChains;
    [SerializeField] private float fadeTime;

    private Animator animator;

    private void OnEnable()
    {
        Inventory.OnItemAddedCallback += ShowItemPickupNotification;
        SaveManager.OnItemUnlockedCallback += ShowItemUnlockNotification;
    }

    private void OnDisable()
    {
        Inventory.OnItemAddedCallback -= ShowItemPickupNotification;
        SaveManager.OnItemUnlockedCallback -= ShowItemUnlockNotification;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void ShowItemPickupNotification(Item item)
    {
        itemNotificationBody.SetActive(true);
        unlockedText.text = "Picked Up";
        brokenChains.SetActive(false);
        itemNameText.text = item.itemName;
        shortDescription.text = item.shortDescription;
        itemIcon.sprite = item.icon;
        StopAllCoroutines();
        StartCoroutine(Animate());

    }

    private void ShowItemUnlockNotification(Item item)
    {
        
        itemNotificationBody.SetActive(true);
        brokenChains.SetActive(true);
        unlockedText.text = "Unlocked";
        itemNameText.text = item.itemName;
        shortDescription.text = item.shortDescription;
        itemIcon.sprite = item.icon;
        StopAllCoroutines();
        StartCoroutine(Animate());

    }

    IEnumerator Animate()
    {
        animator.SetBool("ShowItem", true);
        yield return null;
        animator.SetBool("ShowItem", false);
        yield return new WaitForSeconds(fadeTime);
        itemNotificationBody.SetActive(false);


    }


}
