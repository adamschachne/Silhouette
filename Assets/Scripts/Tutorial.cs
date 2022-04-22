using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public PopupMessage dialogBox;
    void Start()
    {
        CameraControl.ClickEvent += Test;
        dialogBox = GameObject.Find("SolutionManager").GetComponent<PopupMessage>();
        Debug.Log("Inside start");
        if(dialogBox == null) {
           Debug.Log("Inside null"); 
        }
        dialogBox.popUpWithMsg("Click on the box to select it - from script");
    }

    private void Test() {
        Debug.Log("CLICK EVENT ***************");
        dialogBox.closePopUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onDisable() {
        CameraControl.ClickEvent -= Test;
    }

}
