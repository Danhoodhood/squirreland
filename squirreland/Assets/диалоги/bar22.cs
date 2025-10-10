using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bar22 : MonoBehaviour
{
    public GameObject dial;
    public GameObject player;
    public GameObject barmen;
    public bool EndDia = false;
    public TextMeshProUGUI textDialog1;
    int count = 0;
    string dialog = "Джо:\n Спасибо, мужик, выручил. Непросто тебе будет его найти. Его лаборатория находится на неблагополучном дереве, тебе придётся пройти через логово короедов. ";
    string dialog2 = "Джо:\nДля начала нужно выйти с бара через центральный вход и идти прямо, а там увидишь короедов, куда идти дальше разберешься сам.";



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
        
        if (count == 0)
        {
            textDialog1.text = dialog2;
        }
        if (count == 1)
        {
            EndDia = true;
        }

        count++;


    }

    void Start()
    {

        if (EndDia != true)
        {

            dial.SetActive(true);
            player.GetComponent<Player>().enabled = false;
            textDialog1.text = dialog;







        }
    }
}
