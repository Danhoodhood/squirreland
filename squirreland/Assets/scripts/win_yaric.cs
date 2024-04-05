using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class win_yaric : MonoBehaviour
{
    public Text text;
    public GameObject load;
    public void Start()
    {
            
    }
    void Update()
    {
        if (text.text == "4")
        {
            load.GetComponent<LOAD>().load_please();
        }
    }
}
