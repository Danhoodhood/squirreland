using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public GameObject pauseMenu;
    public HeartSystem heartSystem;


    private void OnMouseDown()
    {
        if (!pauseMenu.activeSelf)
        {
            heartSystem.DecreaseHealth();
        }
        
    }
}
