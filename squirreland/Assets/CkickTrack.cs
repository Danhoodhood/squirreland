using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CkickTrack : MonoBehaviour
{

    public HeartSystem heartSystem;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnMouseDown()
    {
        HeartSystem heartSystem = FindObjectOfType<HeartSystem>();

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