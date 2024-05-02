using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicvolume : MonoBehaviour
{
    private AudioSource musicSource;
    public float musicVolume = 1f;
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerPrefs.SetFloat("musicvolume", musicVolume);
        PlayerPrefs.Save();
        musicSource.volume = PlayerPrefs.GetFloat("musicvolume");
    }
    public void SetVolume(float volume)
    {
        musicVolume = volume;
        
    }
}
