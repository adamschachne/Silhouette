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
        popUpBox.SetActive(false);
    }

    public void popUp()
    {
        timer.Pause();
        popUpBox.SetActive(true);
    }

    public void closePopUp()
    {
        timer.Play();
        popUpBox.SetActive(false);
    }
}