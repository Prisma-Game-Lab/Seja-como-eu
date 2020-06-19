using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerControl : MonoBehaviour
{
    public AudioMixer mixer;


    public void SetMasterVolume(float sliderValue)
    {
        mixer.SetFloat("masterVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVolume (float sliderValue)
    {
        mixer.SetFloat("musicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSfxVolume(float sliderValue)
    {
        mixer.SetFloat("sfxVol", Mathf.Log10(sliderValue) * 20);
    }
}
