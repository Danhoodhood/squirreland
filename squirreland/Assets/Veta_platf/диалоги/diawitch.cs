using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class diawitch : MonoBehaviour
{
    public GameObject dial;
    public GameObject player;
    public GameObject startbutton;
    public bool EndDia = false;
    public bool start = false;
    public TextMeshProUGUI textDialog1;
    private string text;
    private int count = 0;
    string[] dialogi = {
        "Бармен:\nДарова, Степаныч, тебе как обычно?",
        "Степаныч:\nДарова, та не, хотелось бы попробовать чего-то нового.",
        "Бармен:\nПридумал тут один новенький коктейль с моим секретным ингредиентом. Не хочешь его попробовать? ",
        "Степаныч:\nНе особо тебе доверяю после твоих прошлых коктейлей. ну а что мне терять, давай попробуем твою новую байду."
        };



    void Update()
    {
        if (EndDia == true)
        {
            dial.SetActive(false);
            player.GetComponent<Player>().enabled = true;




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
            else if (start==true)
            {


                    text = dialogi[count];
                    StartCoroutine(TextCoroutine1());

                    Invoke("dialogskip", 1);
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
            text = dialogi[0];
            StartCoroutine(TextCoroutine1());
            Invoke("dialogskip", 3);






        }
    }
    void dialogskip()
    {
        start = true;


    }


    IEnumerator TextCoroutine1()
    {
        foreach (char abc in text)
        {
            textDialog1.text += abc;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
