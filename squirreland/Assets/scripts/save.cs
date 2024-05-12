using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Save_progress.Save();
        Time.timeScale = 1;
    }

}
