using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer ostAudioMixer;
    private float ostActualVolume;
    public Slider ostVolumeSlider;

    public AudioMixer sfxAudioMixer;
    private float sfxActualVolume;
    public Slider sfxVolumeSlider;

    GameObject[] foodList;
    private AudioSource audioSource;


    void Start()
    {
        ostActualVolume = PlayerPrefs.GetFloat ("ostAudio");
        ostVolumeSlider.value = ostActualVolume;

        sfxActualVolume = PlayerPrefs.GetFloat("sfxAudio");
        sfxVolumeSlider.value = sfxActualVolume;

        foodList = Resources.LoadAll<GameObject>("Prefabs/Food");
        audioSource = GetComponent<AudioSource>();
    }

    public void SetOstVolume(float _volume)
    {

        if (_volume > -40)
        {
            AudioListener.volume = 1f;
            ostAudioMixer.SetFloat("ostVolume", _volume);
        }
        else
            AudioListener.volume = 0f;
        PlayerPrefs.SetFloat("ostAudio", _volume);
    }

    public void SetSFXVolume(float _volume)
    {

        if (_volume > -40)
        {
            AudioListener.volume = 1f;
            sfxAudioMixer.SetFloat("sfxVolume", _volume);
        }
        else
            AudioListener.volume = 0f;
        PlayerPrefs.SetFloat("sfxAudio", _volume);

        PlayRandomSFX();
    }

    private void PlayRandomSFX()
    {
        if (Time.timeSinceLevelLoad > 1.0f)
        {
            string rndItemTag = foodList[Random.Range(0, foodList.Length)].tag;
            AudioClip clip = Resources.Load<AudioClip>("Food_SFX/" + rndItemTag);
            float minPitch = 0.2f;
            float maxPitch = 0.5f;

            if (clip == null)
                Debug.Log("Clip '" + rndItemTag + "' not found");
            else
            {
                audioSource.pitch = 1 + Random.RandomRange(minPitch, maxPitch) * Utils.RandomizePositiveNegative();
                audioSource.clip = clip;
                audioSource.Play();
            }
        }

    }

}
