using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopgame : MonoBehaviour
{
    public bool newobject=true;


    void Update()
    {
        Vector3 position = transform.position;
        if (newobject == false && position.y > 16)
        {
            GameObject spawner = GameObject.FindGameObjectWithTag("spawner");
            Destroy(spawner);
            Application.LoadLevel("Game");



        }
    }
}
