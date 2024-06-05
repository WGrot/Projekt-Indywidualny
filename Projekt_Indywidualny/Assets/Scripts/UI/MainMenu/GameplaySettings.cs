using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameplaySettings : MonoBehaviour
{
    [SerializeField] private Slider mouseSensitivitySlider;

    public void UpdateSensitivityValueOnChange()
    {
        PlayerPrefs.SetFloat("M_Sensitivity", mouseSensitivitySlider.value);
        PlayerPrefs.Save();
    }
}
