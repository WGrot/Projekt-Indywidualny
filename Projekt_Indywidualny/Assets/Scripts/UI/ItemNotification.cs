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

    [SerializeField] private AudioClip unlockSound;
    [SerializeField] private AudioClip pickupSound;

    private AudioSource audioSource;
    private Animator animator;

    private void OnEnable()
    {
        Inventory.OnItemAddedCallback += ShowItemPickupNotification;
        Inventory.OnWeaponAddedCallback += ShowWeaponPickupNotification;
        SaveManager.OnItemUnlockedCallback += ShowItemUnlockNotification;
        SaveManager.OnWeaponUnlockedCallback += ShowItemUnlockNotification;
    }

    private void OnDisable()
    {
        Inventory.OnItemAddedCallback -= ShowItemPickupNotification;
        Inventory.OnWeaponAddedCallback -= ShowWeaponPickupNotification;
        SaveManager.OnItemUnlockedCallback -= ShowItemUnlockNotification;
        SaveManager.OnWeaponUnlockedCallback -= ShowItemUnlockNotification;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void ShowItemPickupNotification(Item item)
    {
        itemNotificationBody.SetActive(true);
        audioSource.clip = pickupSound;
        audioSource.Play();
        unlockedText.text = "Picked Up";
        brokenChains.SetActive(false);
        itemNameText.text = item.itemName;
        if(item is WeaponSO)
        {
            shortDescription.text = Inventory.Instance.GetPrefixOfActiveWeapon().PrefixName;
        }
        else
        {
            shortDescription.text = item.shortDescription;
        }

        itemIcon.sprite = item.icon;
        StopAllCoroutines();
        StartCoroutine(Animate());

    }

    private void ShowWeaponPickupNotification(WeaponSO weapon)
    {
        itemNotificationBody.SetActive(true);
        audioSource.clip = pickupSound;
        audioSource.Play();
        unlockedText.text = "Picked Up";
        brokenChains.SetActive(false);
        itemNameText.text = weapon.itemName;
        shortDescription.text = Inventory.Instance.GetPrefixOfActiveWeapon().PrefixName;
        itemIcon.sprite = weapon.icon;
        StopAllCoroutines();
        StartCoroutine(Animate());

    }



    private void ShowItemUnlockNotification(Item item)
    {
        
        itemNotificationBody.SetActive(true);
        audioSource.clip = unlockSound;
        audioSource.Play();
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
