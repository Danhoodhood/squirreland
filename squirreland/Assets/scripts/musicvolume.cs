using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicvolume : MonoBehaviour
{
    private AudioSource musicSource;
    private float musicVolume=1f;
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        musicSource.volume = musicVolume;   
    }
    public void SetVolume(float volume)
    {
        musicVolume = volume;
    }
}
