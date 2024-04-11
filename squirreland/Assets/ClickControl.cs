using UnityEngine;
using UnityEngine.UI;

public class ClickControl : MonoBehaviour
{
    public static string obj_name;
    public static int count;// количество желудей 
    public Text text;
    public AudioSource audioSource;

    void Start()
    {
        timer_for_hide.startTime = 60;
        Time.timeScale = 1;
    }

    void Update()
    {

    }

    void OnMouseDown()//при клике
    {
        obj_name = gameObject.name;
        //Debug.Log(obj_name);

        if (count < 4)//если количество желудей меньше 4х
        {
            audioSource.Play();//звук нажатия на желудь
            Destroy(gameObject);//удаляется желудь
            count++;
            text.text = count.ToString();//вывод найденных желудей
            
        }
    }

}
