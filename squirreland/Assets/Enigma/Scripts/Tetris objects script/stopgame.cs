using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("Game");



        }
    }
}
