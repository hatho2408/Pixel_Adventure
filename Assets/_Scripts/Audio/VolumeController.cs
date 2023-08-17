using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField]private string mixerParameters;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;
    [SerializeField] private float sliderMultiplier;

   

    public void SetupVolume()
    {
        slider.onValueChanged.AddListener(SliderValue);
        slider.minValue = .001f;
        slider.value = PlayerPrefs.GetFloat(mixerParameters, slider.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(mixerParameters,slider.value);
    }

    private void SliderValue(float value)
    {
       audioMixer.SetFloat(mixerParameters, Mathf.Log10(value) * sliderMultiplier);

    }
}
