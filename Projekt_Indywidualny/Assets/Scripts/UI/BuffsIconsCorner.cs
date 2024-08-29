using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffsIconsCorner : MonoBehaviour
{
    [SerializeField] List<GameObject> iconSlots;

    private void OnEnable()
    {
        BuffManager.OnBuffsUpdatedCallback += UpdateIconSlots;
    }

    private void OnDisable()
    {
        BuffManager.OnBuffsUpdatedCallback -= UpdateIconSlots;
    }



    public void UpdateIconSlots()
    {
        int i = 0;

        foreach (Buff buff in BuffManager.Instance.BuffsWithIcons)
        {
            iconSlots[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
            iconSlots[i].GetComponent<Image>().sprite = buff.BuffIcon;
            i++;

            if (i >= iconSlots.Count)
            {
                break;
            }
        }

        for (; i < iconSlots.Count; i++)
        {
            iconSlots[i].GetComponent<Image>().color = new Color(1,1,1,0f);
        }
    }

}
