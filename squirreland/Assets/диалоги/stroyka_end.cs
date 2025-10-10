using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class stroyka_end : MonoBehaviour
{
    public GameObject dial;
    public GameObject player;
    public GameObject gg;
    public GameObject barmen;
    public GameObject perexod;
    public bool EndDia = false;
    public TextMeshProUGUI textDialog3;
    private string text;
    private int count = 0;
    string[] dialogi = {
        "Степаныч:\nСтену я построил, а как выбраться я все еще не знаю.  ",
        "Кенни: \nТебе надо к нашему ученому, его зовут Билл, я сейчас не знаю где он, но он часто сидит в баре в соседнем дереве. ",
        "Степаныч:\nСпасибо, тебе, узнаю там."
        };



    void Update()
    {
        if (EndDia == true)
        {
            dial.SetActive(false);
            player.GetComponent<Player>().enabled = true;
            perexod.SetActive(true);
        }





    }
    public void skip()
    {
        if (EndDia != true)
        {
            count++;
            textDialog3.text = "";
            if (count == 3)
            {
                EndDia = true;
            }

            if (count == 1)
            {
                gg.SetActive(false);
                barmen.SetActive(true);
                textDialog3.text = dialogi[count];

            }
            if (count == 2)
            {

                gg.SetActive(true);
                barmen.SetActive(false);
                textDialog3.text = dialogi[count];

            }








        }

    }


    private void Start()
    {
        dial.SetActive(true);
        player.GetComponent<Player>().enabled = false;
        textDialog3.text = dialogi[0];
    }
    







        
    



}
