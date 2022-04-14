using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    private static float startTime = 0.0f;
    private bool pause = false;
    private float pauseTimeStart;
    private float pausedTime;
    private static float t = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        // if(startTime < 0.001f) {
        //     startTime = Time.time;
        // }
        // pausedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(pause) {
            return;
        }
        // float t = Time.deltaTime;
        // float t = Time.time - startTime - pausedTime;
        t += Time.deltaTime;
        string minutes = ((int) t / 60 ).ToString();
        string seconds = (t%60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
    }

    public void Pause()
    {
        pause = true;
        // pauseTimeStart = Time.time;
    }

    public void Play()
    {
        pause = false;
        // pausedTime += Time.time - pauseTimeStart;
    }

    public void resetTimer()
    {
        // Debug.Log("start" + startTime);
        // startTime = 0.0f;
        // pausedTime = 0.0f;
        t = 0.0f;
        // Debug.Log(" start after pause "+startTime);
    }
}
