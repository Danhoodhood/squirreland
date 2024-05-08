using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class diawitch : MonoBehaviour
{
    public GameObject dial;
    public GameObject player;
    public bool EndDia = false;
    public bool start = false;
    public TextMeshProUGUI textDialog1;
    public TextMeshProUGUI textDialog2;
    private string text;
    private int count = 0;
    string[] dialogi = {
        "Бармен:\nДарова, та не, хотелось бы попробовать чего-то нового.",
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
            textDialog2.text = "";
            if (count == 4)
            {
                EndDia = true;
            }
            else if (start==true)
            {

                if (count == 3)
                {
                    text = dialogi[count];
                    StartCoroutine(TextCoroutine1());

                    Invoke("dialogskip", 1);
                }
                if (count == 1 || count == 2)
                {
                    text = dialogi[count];
                    StartCoroutine(TextCoroutine2());
                    Invoke("dialogskip", 3);
                }
            }



            start = false;


        }
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
    IEnumerator TextCoroutine2()
    {
        foreach (char abc in text)
        {
            textDialog2.text += abc;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
