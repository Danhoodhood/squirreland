using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer_for_hide : MonoBehaviour
{
    public static float startTime = 60;
    public Text timeLabel;

    // Start is called before the first frame update
    void Start()
    {
        timeLabel.text = startTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        startTime -= Time.deltaTime;

        if (startTime <= 0)
        {
            startTime = 0;
            // ƒополнительные действи€ при достижении нул€
        }

        timeLabel.text = Mathf.Round(startTime).ToString();
    }
}
