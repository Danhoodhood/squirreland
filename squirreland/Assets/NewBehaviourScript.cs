using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalScore.score = 0;
        TMP_Text str_score = GameObject.FindGameObjectWithTag("score").GetComponent<TMP_Text>();
        str_score.text = GlobalScore.score.ToString();
    }
}
