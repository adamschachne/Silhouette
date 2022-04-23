using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private static int stepCount = 0;
    private int NUM_STEPS = 3;

    private List<TutorialStepInfo> steps = new List<TutorialStepInfo>();
    // Start is called before the first frame update
    public PopupMessage dialogBox;
    void Start()
    {
        CameraControl.ClickEvent += Test;
        CameraControl.DeselectEvent += Deselect;
        PlayerMovement.ButtonClickEvent += displayEvent;
        playerMovement = GameObject.Find("GameManager").GetComponent<PlayerMovement>();
        // GameObject resetBtn = GameObject.Find("Reset").GetComponent<PlayerMovement>();
        dialogBox = GameObject.Find("SolutionManager").GetComponent<PopupMessage>();
        Debug.Log("Inside start");
        if(dialogBox == null) {
           Debug.Log("Inside null"); 
        }
        dialogBox.popUpWithMsg("Click on the block to select it!");
        initializeSteps(); 
    }

    private void Test() {
        if(stepCount == 0){
        Debug.Log("CLICK EVENT ***************");
        displayEvent();
        }
    }

    private void Deselect() {
        playerMovement.ResetMovementControl();
        dialogBox.closePopUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void displayEvent() {
        if(stepCount >= NUM_STEPS) {
            return;
        }
        Debug.Log("JDLDFJLWFJIJLSF");
        Debug.Log("Step count ");
        Debug.Log(stepCount);
        string btn = steps[stepCount].getButtonName();
        string msg =steps[stepCount].getMessage(); 
        Vector3 pos= steps[stepCount].getPanelPos();
        Debug.Log(msg);
        playerMovement.BlinkBtn(btn, "none");
        dialogBox.closePopUp();
        dialogBox.popUpWithMsg(msg);
        MoveSayDialog(pos);
        stepCount++;
    }

    public void MoveSayDialog(Vector3 vectorPos)
    {
        var sayDialog = GameObject.Find("DialogueBox").transform.Find("Panel").GetComponent<RectTransform>();
        var pos = sayDialog.localPosition;
        sayDialog.localPosition = vectorPos;
    }
    private void onDisable() {
        CameraControl.ClickEvent -= Test;
        CameraControl.DeselectEvent -= Deselect;
        PlayerMovement.ButtonClickEvent -= displayEvent;
    }

    private void initializeSteps() {

        var sayDialog = GameObject.Find("DialogueBox").transform.Find("Panel").GetComponent<RectTransform>();
        var pos = sayDialog.localPosition;
        steps.Add(new TutorialStepInfo("Click on the blinking right arrow", "right",new Vector3(pos.x-30, pos.y-80, pos.z)));
        steps.Add(new TutorialStepInfo("Rotate the box now!", "clockwise", new Vector3(pos.x+100, pos.y, pos.z)));  
        steps.Add(new TutorialStepInfo("Move the box to match the pattern on the wall", "right", new Vector3(pos.x+250, pos.y+57, pos.z)));
    }

}

public class TutorialStepInfo {
    private string buttonName;
    private string message;

    private Vector3 pos;


    public TutorialStepInfo(string msg, string btn, Vector3 vectorPos) {
        buttonName = btn;
        message = msg; 
        pos = vectorPos;
    }
    // public setValues(string msg, string btn) {
    //     buttonName = btn;
    //     message = msg;
    // }

    public string getButtonName() {
        return buttonName;
    }

    public string getMessage() {
        return message;
    }

    public Vector3 getPanelPos() {
        return pos;
    }
}
