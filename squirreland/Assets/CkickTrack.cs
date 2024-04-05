using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CkickTrack : MonoBehaviour
{

    public HeartSys heartSystem;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnMouseDown()
    {

        if (heartSystem != null)
        {
            heartSystem.DecreaseHealth();
        }
        else
        {
            Debug.LogError("HeartSystem не найден!");
        }
    }
}