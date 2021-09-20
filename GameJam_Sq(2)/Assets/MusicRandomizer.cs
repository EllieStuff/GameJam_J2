using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRandomizer : MonoBehaviour
{
    public AudioClip[] songs = new AudioClip[5];

    void Start()
    {
        int rndSongId = Random.RandomRange(0, 5);
        while(PlayerPrefs.GetInt("lastRndSongId", -1) == rndSongId)
        {
            rndSongId = Random.RandomRange(0, 5);
        }
        PlayerPrefs.SetInt("lastRndSongId", rndSongId);


        GetComponent<AudioSource>().clip = songs[rndSongId];
        GetComponent<AudioSource>().Play();
    }


}
