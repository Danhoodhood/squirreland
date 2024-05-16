using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicvolume : MonoBehaviour
{
    private AudioSource musicSource;
    private float musicVolume=1f;
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("musicvolume"))
        {
            musicSource.volume = PlayerPrefs.GetFloat("musicvolume");
        }
        else
        {
            musicSource.volume = 1f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        musicSource.volume = musicVolume;
        
       // musicSource.volume = PlayerPrefs.GetFloat("musicvolume");
    }
    public void SetVolume(float volume)
    {
        musicVolume = volume;
        PlayerPrefs.SetFloat("musicvolume", volume);
        PlayerPrefs.Save();

    }
}
