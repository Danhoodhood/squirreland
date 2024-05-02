using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundvolume : MonoBehaviour
{
    private AudioSource musicSource;
    public float musicVolume=1f;
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerPrefs.SetFloat("soundvolume", musicVolume);
        PlayerPrefs.Save();
        musicSource.volume = PlayerPrefs.GetFloat("soundvolume");
    }
    public void SetVolume(float volume)
    {
        musicVolume = volume;
        
    }
}
