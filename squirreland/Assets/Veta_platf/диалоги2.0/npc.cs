using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;


public class npc : MonoBehaviour
{
    public GameObject dial;
    public bool EndDial = false;
    public bool start = false;
    public float invoke_delay;
    public TextMeshProUGUI textDialog;
    public GameObject player;
    private string text;



    void Update()
    {

        if (EndDial == true)
        {

            dial.SetActive(false);
            player.GetComponent<Player>().enabled = true;



        }
        
        if ((Input.GetMouseButtonDown(0) && start == true) || (Input.GetKeyDown(KeyCode.Space) && start == true))
        {
            EndDial = true;

        }


    }
    void OnTriggerEnter2D(Collider2D col)
    {
       
        if (EndDial != true)
        {
            dial.SetActive(true);
            player.GetComponent<Player>().enabled = false;

            text = "Дениска\n#@$^&*$#@&! К чёрту! ";
            StartCoroutine(TextCoroutine());
            Invoke("dialogskip", invoke_delay);
           

        }

    }
    void dialogskip()
    {
        start = true;

    }


    IEnumerator TextCoroutine()
    {
        foreach(char abc in text)
        {
            textDialog.text += abc;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
