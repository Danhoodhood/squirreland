using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class diawitch2 : MonoBehaviour
{
    public GameObject dial;
    public GameObject player;
    public GameObject startbutton;
    public GameObject gg;
    public GameObject barmen;
    public GameObject music;
    public GameObject sound;
    public GameObject drink;
    public GameObject glotok;
    public bool EndDia = false;
    public bool start = false;
    public TextMeshProUGUI textDialog1;
    public TextMeshProUGUI textDialog2;
    private string text;
    private int count = 0;
    string[] dialogi = {
        "Бармен:\nНу как тебе мой коктейль?",
        "Степаныч:\nИди ты в дупло со своим коктейлями.",
        };



    void Update()
    {
        if (EndDia == true)
        {
            dial.SetActive(false);
            drink.SetActive(true);
            music.SetActive(false);
            sound.GetComponent<AudioSource>().Stop();
            text = "А ведь все могло закончиться по-плохому для Степаныча из-за лишнего бокальчика… надо ли оно вам?";
            StartCoroutine(TextCoroutine2());
            Invoke("sceneloade", 4);
            glotok.SetActive(true);
            EndDia =false;




        }


        


    }
    public void skip()
    {
        if (EndDia != true)
        {
            count++;
            textDialog1.text = "";
            if (count == 4)
            {
                EndDia = true;
            }

            if(count == 1) 
            {
                gg.SetActive(true);
                barmen.SetActive(false);
                textDialog1.text = dialogi[count];

            }
            if (count == 2)
            {
                
                gg.SetActive(false);
                barmen.SetActive(true);
                textDialog1.text = dialogi[count];
 
            }
            if (count == 3)
            {
                gg.SetActive(true);
                barmen.SetActive(false);
                textDialog1.text = dialogi[count];

            }


            



            start = false;


        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (EndDia != true)
        {
            startbutton.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {

            startbutton.SetActive(false);

    }
    public void startdialog() 
    {

        if (EndDia != true)
        {

            dial.SetActive(true);
            player.GetComponent<Player>().enabled = false;
            textDialog1.text = dialogi[0];







        }
    }




    IEnumerator TextCoroutine2()
    {
        foreach (char abc in text)
        {
            textDialog2.text += abc;
            yield return new WaitForSeconds(0.05f);

        }
    }
}
