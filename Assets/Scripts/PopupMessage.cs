using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupMessage : MonoBehaviour
{

    public GameObject popUpBox;
    public Timer timer;

    void Start()
    {
        // popUpBox.SetActive(false);
    }

    public void popUp()
    {
        // timer.Pause();
        popUpBox.SetActive(true);
    }


    public void popUpWithMsg(string message)
    {
        // timer.Pause();
        popUpBox.SetActive(true);
        if (popUpBox.activeSelf && !string.IsNullOrEmpty (message)) {
            // Text textObject = popUpBox.gameObject.GetComponentInChildren<Text> ();
            TextMeshProUGUI textObject = popUpBox.gameObject.GetComponentInChildren<TextMeshProUGUI> (); 
            //  Text textObject = popUpBox.gameObject.Find("Keys Info").GetComponent<Text>();
            textObject.text = message;
            Debug.Log(textObject.text);
        }
    }
    public void closePopUp()
    {
        // timer.Play();
        popUpBox.SetActive(false);
    }
}