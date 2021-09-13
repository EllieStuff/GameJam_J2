using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRandomizer : MonoBehaviour
{
    public AudioClip[] songs = new AudioClip[5];

    void Start()
    {
        GetComponent<AudioSource>().clip = songs[Random.RandomRange(0, 5)];
        GetComponent<AudioSource>().Play();
    }


}
