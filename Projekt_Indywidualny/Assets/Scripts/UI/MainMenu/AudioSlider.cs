using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioSlider : MonoBehaviour
{
    Slider slider
    {
        get { return GetComponent<Slider>(); }
    }

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string volumeName;

    public void UpdateValueOnChange()
    {
        mixer.SetFloat(volumeName, Mathf.Log(slider.value +0.01f) * 20);
    }
}
