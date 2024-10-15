using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : MonoBehaviour
{
    [SerializeField] private List<GameObject> settingsBoxes;
    private GameObject activeBox;
    Resolution[] resolutions;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    int currentResolutionIndex = 0;
	private void Start()
	{
        ClearAndLoadResolutions();
	}
	public void switchBox(int number)
    {
        if(activeBox != null)
        {
			activeBox.SetActive(false);
		}
        activeBox = settingsBoxes[number];
		activeBox.SetActive(true);
	}

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

    private void ClearAndLoadResolutions()
    {
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions();

		List<string> resolutionStrings = new List<string>();
        for (int i = 0; i< resolutions.Length; i++)
        {
            Resolution resolution = resolutions[i];
			string option = resolution.width.ToString() + "x" + resolution.height.ToString() + " " + resolution.refreshRateRatio.ToString() + "Hz";
			resolutionStrings.Add(option);

            if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
		}

		resolutionDropdown.AddOptions(resolutionStrings);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
	}
}
