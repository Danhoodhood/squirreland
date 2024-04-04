using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenu : MonoBehaviour
{
    public void Start()
    {
        Save_progress.Save();
    }
    // Start is called before the first frame update
    public void Play()
    {
        Application.LoadLevel("Game");
    }
}
