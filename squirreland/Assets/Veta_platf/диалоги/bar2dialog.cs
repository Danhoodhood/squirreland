using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bar2dialog : MonoBehaviour
{
    public GameObject dial;
    public GameObject player;
    public GameObject startbutton;
    public GameObject gg;
    public GameObject barmen;
    public bool EndDia = false;
    public bool start = false;
    public TextMeshProUGUI textDialog1;
    private string text;
    private int count = 0;
    string[] dialogi = {
        "Степаныч:\n Привет, мне сказали, что тут часто бывает Билл, как мне его найти?",
        "Джо: \nПривет, мой друг, по традиции всем новым гостям мой новый фирменный коктейль с секретным ингредиентом.",
        "Степаныч:\n Кого-то ты мне напоминаешь, снова на такое я не поведусь. Так ты помоежешь мне найти Билла ?",
        "Джо:\n Да, помоги мне собрать жёлуди на другой части бара, а потом поговорим."
        };



    void Update()
    {
        if (EndDia == true)
        {
            dial.SetActive(false);
            startbutton.SetActive(false);
            player.GetComponent<Player>().enabled = true;




        }





    }
    public void skip()
    {
        if (EndDia != true && start == true)
        {
            count++;
            textDialog1.text = "";
            if (count == 4)
            {
                EndDia = true;
            }

            if (count == 1)
            {
                gg.SetActive(true);
                barmen.SetActive(false);
                text = dialogi[count];
                StartCoroutine(TextCoroutine1());
                Invoke("dialogskip", 5);
            }
            if (count == 2)
            {

                gg.SetActive(false);
                barmen.SetActive(true);
                text = dialogi[count];
                StartCoroutine(TextCoroutine1());
                Invoke("dialogskip", 5);
            }
            if (count == 3)
            {
                gg.SetActive(true);
                barmen.SetActive(false);
                text = dialogi[count];
                StartCoroutine(TextCoroutine1());
                Invoke("dialogskip", 4);
            }






            start = false;


        }

    }
    void sceneloade()
    {
        SceneManager.LoadScene("Heart_bit");
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
            Invoke("dialogskip", 4);






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
