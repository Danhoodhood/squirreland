using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiButtonOn : MonoBehaviour
{

    public AudioSource soundPlay;

    public void PlayThisSoungEffect()
    {
        soundPlay.Play();
    }
}
