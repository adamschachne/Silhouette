using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    public PopupMessage dialogBox;
    void Start()
    {
        CameraControl.ClickEvent += Test;
        CameraControl.DeselectEvent += Deselect;
        playerMovement = GameObject.Find("GameManager").GetComponent<PlayerMovement>();
        dialogBox = GameObject.Find("SolutionManager").GetComponent<PopupMessage>();
        Debug.Log("Inside start");
        if(dialogBox == null) {
           Debug.Log("Inside null"); 
        }
        dialogBox.popUpWithMsg("Click on the box to select it - from script");
    }

    private void Test() {
        Debug.Log("CLICK EVENT ***************");
        playerMovement.BlinkBtn("right", "none");
    }

    private void Deselect() {
        playerMovement.ResetMovementControl();
        dialogBox.closePopUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onDisable() {
        CameraControl.ClickEvent -= Test;
        CameraControl.DeselectEvent -= Deselect;
    }

}
