using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundvolume : MonoBehaviour
{
    public Slider slider;
    private AudioSource audioSource;
    private float savedVolume = 1f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Загрузка сохраненного значения громкости или установка по умолчанию
        if (PlayerPrefs.HasKey("Volume"))
        {
            savedVolume = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", savedVolume);
            PlayerPrefs.Save();
        }

        // Установка положения слайдера и громкости аудио
        slider.value = savedVolume;
        audioSource.volume = savedVolume;

        // Добавление слушателя для изменения громкости
        slider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    public void ChangeVolume()
    {
        audioSource.volume = slider.value;
        savedVolume = slider.value;
        PlayerPrefs.SetFloat("Volume", savedVolume);
        PlayerPrefs.Save();
    }
}
