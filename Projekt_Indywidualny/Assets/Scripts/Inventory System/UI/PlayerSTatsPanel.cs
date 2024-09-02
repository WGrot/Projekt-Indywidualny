using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class PlayerSTatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueTextBox;
    [SerializeField] private int importantStats = 6;

    private void OnEnable()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i< importantStats; i++ )
        {
            CharacterStat stat = PlayerStatus.Instance.stats[i];
            sb.Append(stat.value);
            sb.Append("<br>");

        }
        valueTextBox.SetText(sb.ToString());
    }
    
}
