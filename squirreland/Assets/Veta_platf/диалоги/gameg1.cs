using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameg1 : MonoBehaviour
{
    public GameObject startbutton;
    public bar2dialog endbutton;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        if (endbutton.EndDia == true)
        {
            startbutton.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {

        startbutton.SetActive(false);

    }
    public void startgame()
    {
        SceneManager.LoadScene("Platf_level3");
    }
}
