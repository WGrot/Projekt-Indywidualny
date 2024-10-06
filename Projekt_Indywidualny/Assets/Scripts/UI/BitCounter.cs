using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BitCounter : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI amount;
    [SerializeField] private GameObject counterBody;
    [SerializeField] private float bitCounterLifeTime;
    bool isCounterActive = false;

    public void OnEnable()
    {
        BitCounterShowTrigger.OnLookedAtTriggerCallback += ShowCounterOne;
        SaveManager.OnBitsChangedCallback += UpdateAmount;
    }


    public void OnDisable()
    {
        BitCounterShowTrigger.OnLookedAtTriggerCallback -= ShowCounterOne;
        SaveManager.OnBitsChangedCallback += UpdateAmount;
    }

    public void ShowCounterOne()
    {
        if (!isCounterActive)
        {
            StopAllCoroutines();
            StartCoroutine(ShowCounter());
        }

    }


    void UpdateAmount()
    {
        amount.text = SaveManager.Instance.GetCollectedBits().ToString();
    }
    IEnumerator ShowCounter()
    {
        isCounterActive = true;
        counterBody.SetActive(true);
        amount.text = SaveManager.Instance.GetCollectedBits().ToString();
        yield return new WaitForSeconds(bitCounterLifeTime);
        counterBody.SetActive(false);
        isCounterActive= false;

    }
}
