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
    [SerializeField] private Image itemIcon;
    [SerializeField] private float fadeTime;

    private Animator animator;

    private void OnEnable()
    {
        Inventory.OnItemAddedCallback += ShowItemNotification;
    }

    private void OnDisable()
    {
        Inventory.OnItemAddedCallback -= ShowItemNotification;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void ShowItemNotification(Item item)
    {
        itemNotificationBody.SetActive(true);
        itemNameText.text = item.itemName;
        shortDescription.text = item.shortDescription;
        itemIcon.sprite = item.icon;
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
