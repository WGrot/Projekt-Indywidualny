using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolChangeBox : MonoBehaviour
{
    [SerializeField] private GameObject PoolContainer;
    [SerializeField] private GameObject poolList;

    /*
    private void OnEnable()
    {
        int totalNumberOfPools = Enum.GetNames(typeof(ItemPools)).Length;

        for (int i = 0; i< totalNumberOfPools; i++)
        {
            PoollContainer script = Instantiate(PoolContainer, poolList.transform).GetComponent<PoollContainer>();
            script.SetName(((ItemPools)i).ToString());
        }
    }
    */
}
