using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public GameObject c;
    public GameObject d;


    void Start()
    {
        a.SetActive(true); 
        b.SetActive(true);
        c.SetActive(true); 
        d.SetActive(true);
    }
}
