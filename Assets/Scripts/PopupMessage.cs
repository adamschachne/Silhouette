using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupMessage : MonoBehaviour
{

    public GameObject popUpBox;

    void Start()
    {
        popUpBox.SetActive(false);
    }

    public void popUp()
    {
        popUpBox.SetActive(true);
    }

    public void closePopUp()
    {
        popUpBox.SetActive(false);
    }
}