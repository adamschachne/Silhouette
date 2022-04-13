using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    private float startTime = Time.time;
    private bool pause = false;
    private float pauseTimeStart;
    private float pausedTime;
    // Start is called before the first frame update
    void Start()
    {
        // string username = timerText.GetComponent<TMP_Text>().text;
        // Debug.Log(startTime);
        // Debug.Log(username);
        // startTime = Time.time;
        pausedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(pause) {
            return;
        }
        float t = Time.time - startTime - pausedTime;
        string minutes = ((int) t / 60 ).ToString();
        string seconds = (t%60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
    }

    public void Pause()
    {
        pause = true;
        pauseTimeStart = Time.time;
    }

    public void Play()
    {
        pause = false;
        pausedTime += Time.time - pauseTimeStart;
    }

    public void resetTimer()
    {
        Debug.Log("1234");
        string username = timerText.GetComponent<TMP_Text>().text;
        Debug.Log("start" + startTime);
        Debug.Log("usr name" + username);
        startTime = Time.time;
        pausedTime = 0.0f;
        Debug.Log(" start after pause "+startTime);
    }
}
