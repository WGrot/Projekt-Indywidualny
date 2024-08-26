using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PoollContainer : MonoBehaviour
{
    [SerializeField] private Toggle doesBelongToPool;
    [SerializeField] private TextMeshProUGUI poolName;


    public void SetName(string name)
    {
        poolName.text = name;
    }

    public void SetToggleValue(bool value)
    {
        doesBelongToPool.isOn = value;
    }

    public bool GetToggleValue()
    {
        return doesBelongToPool.isOn;
    }
}
