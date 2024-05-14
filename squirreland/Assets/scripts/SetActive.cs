using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public GameObject c;
    public GameObject d;
    public GameObject e;


    void Start()
    {
        Time.timeScale = 0f;
        a.SetActive(true); 
        b.SetActive(true);
        c.SetActive(true); 
        d.SetActive(true);
        e.SetActive(true);
    }
}
