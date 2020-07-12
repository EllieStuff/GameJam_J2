using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    private float actualVolume;
    public Slider VolumeSlider;
    void Start()
    {
        actualVolume = PlayerPrefs.GetFloat ("audio");
        VolumeSlider.value = actualVolume;
    }

    public void SetVolume(float volume)
    {

        if (volume > -40)
        {
            AudioListener.volume = 1f;
            audioMixer.SetFloat("volume", volume);
        }
        else
            AudioListener.volume = 0f;
        PlayerPrefs.SetFloat("audio", volume);
    }

}
