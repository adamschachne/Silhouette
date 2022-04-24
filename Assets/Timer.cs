using System.Collections;
 using System.Collections.Generic;
 using UnityEngine.UI;
 using UnityEngine;
 using TMPro;

 public class Timer : MonoBehaviour
 {

     public TextMeshProUGUI timerText;
     private bool pause = false;
     private static float timeToDisplay = 0.0f;
     // Start is called before the first frame update
     void Start()
     {
     }

     // Update is called once per frame
     void Update()
     {
         if(pause) {
             return;
         }
         timeToDisplay += Time.deltaTime;
         timerText.text = this.GetElapsedTimeToDisplay();
     }

     public void Pause()
     {
         timerText.text = "";
         pause = true;
     }

     public void Play()
     {
         timerText.text = this.GetElapsedTimeToDisplay();
         pause = false;
     }

     public void ResetTimer()
     {
         timeToDisplay = 0.0f;
     }

     public float GetElapsedTime() {
         return timeToDisplay;
     }

     public string GetElapsedTimeToDisplay() {
         float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
         float seconds = Mathf.FloorToInt(timeToDisplay % 60);
         return string.Format("{0:00}:{1:00}", minutes, seconds);
     }
 }