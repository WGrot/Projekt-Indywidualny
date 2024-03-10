using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class PlayerSTatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueTextBox;


    private void OnEnable()
    {
        StringBuilder sb = new StringBuilder();
        foreach(CharacterStat stat in PlayerStatus.Instance.stats)
        {
            sb.Append(stat.value);
            sb.Append("<br>");
        }
        valueTextBox.SetText(sb.ToString());
    }

}
