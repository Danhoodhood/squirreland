using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Check_click : MonoBehaviour
{
    public AudioSource audioSourcePoint;

    public void OnClick()
    {

        GameObject[] dots = GameObject.FindGameObjectsWithTag("point");
        bool liy = true;
        if (dots is null)
        {
            GetComponent<Image>().color = Color.red;
            StartCoroutine(ResetColorAfterDelay());
            GlobalScore.score--;
        }
        foreach (GameObject i in dots)
        {
            RectTransform rt = GetComponent<RectTransform>();
            float width1 = rt.sizeDelta.x * rt.localScale.x / 2f;
            float height1 = rt.sizeDelta.y * rt.localScale.y / 2f;
            RectTransform rt2 = i.GetComponent<RectTransform>();
            float width2 = rt2.sizeDelta.x * rt2.localScale.x / 2f;
            float height2 = rt2.sizeDelta.y * rt2.localScale.y / 2f;

            if (transform.position.x - width1 - width2 <= i.transform.position.x && 
                i.transform.position.x <= transform.position.x + width1 + width2 &&
                transform.position.y - height1 - height2 <= i.transform.position.y && 
                i.transform.position.y <= transform.position.y + height1 + height2)
            {
                var sr = i.GetComponent<Image>();
                Color color = sr.color;
                color.a = 1f;
                sr.color = color;
                GlobalScore.score++;
                i.SetActive(false);                        
                    liy = false;
                    GetComponent<Image>().color = Color.green;
                audioSourcePoint.Play();
                    StartCoroutine(ResetColorAfterDelay());
             }
             
            
        }
        if (liy)
        {
                GetComponent<Image>().color = Color.red;
                StartCoroutine(ResetColorAfterDelay());
                GlobalScore.score--;
            
        }
        if (GlobalScore.score<0)
        {
            GlobalScore.score = 0;
        }
        TMP_Text str_score = GameObject.FindGameObjectWithTag("score").GetComponent<TMP_Text>();
        str_score.text = GlobalScore.score.ToString();

    }
    IEnumerator ResetColorAfterDelay()
    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<Image>().color = Color.white; // assuming default color is white
    }
}
