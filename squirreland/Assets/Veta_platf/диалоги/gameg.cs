using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameg : MonoBehaviour
{
    public GameObject startbutton;
    public string namelvl;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {

            startbutton.SetActive(true);

    }
    void OnTriggerExit2D(Collider2D col)
    {

        startbutton.SetActive(false);

    }
    public void startgame()
    {
        SceneManager.LoadScene(namelvl);
    }
}
