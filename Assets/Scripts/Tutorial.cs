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

    private Vector3Int UP = new Vector3Int(1, 0, 0);
    private Vector3Int DOWN = new Vector3Int(-1, 0, 0);
    private Vector3Int LEFT = new Vector3Int(0, 0, 1);
    private Vector3Int RIGHT = new Vector3Int(0, 0, -1);

    private Vector3Int NONE = new Vector3Int(1,1,1);

    private Vector3 CLOCKWISE = 90 * Vector3.up;
    private Vector3 COUNTERCLOCKWISE = -90 * Vector3.up;
    private Vector3 NOROTATE = 0 * Vector3.up;

    void Start()
    {
        CameraControl.ClickEvent += ClickBlockEvent;
        CameraControl.DeselectEvent += Deselect;
        PlayerMovement.ButtonClickEvent += DisplayEvent;
        Debug.Log("Tutorial start");
        playerMovement = GameObject.Find("GameManager").GetComponent<PlayerMovement>();
        dialogBox = GameObject.Find("SolutionManager").GetComponent<PopupMessage>();
        
        if(dialogBox == null) {
           Debug.Log("Inside null"); 
        }
        dialogBox.popUpWithMsg("Click on the block to select it!");
        playerMovement.SetEnableTutorial(true);
        playerMovement.DisableResetButton();
        CameraControl.DisableBtns();
        initializeSteps(); 
    }

    void OnDestroy() {
        CameraControl.ClickEvent -= ClickBlockEvent;
        CameraControl.DeselectEvent -= Deselect;
        PlayerMovement.ButtonClickEvent -= DisplayEvent;        
    }

    private void ClickBlockEvent() {
        if(stepCount == 0){
            DisplayEvent();
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

    void DisplayEvent() {
        if(stepCount >= NUM_STEPS) {
            return;
        }
        string btn = steps[stepCount].getButtonName();
        string msg =steps[stepCount].getMessage(); 
        Vector3 pos= steps[stepCount].getPanelPos();
        Vector3Int nxtBtn = steps[stepCount].getNextBtn();
        Vector3 nxtRBtn = steps[stepCount].getNextRBtn();
        Debug.Log(msg);
        playerMovement.BlinkBtn(btn, "none");
        playerMovement.SetAllowMovement(nxtBtn);
        playerMovement.SetAllowRotate(nxtRBtn);
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
        CameraControl.ClickEvent -= ClickBlockEvent;
        CameraControl.DeselectEvent -= Deselect;
        PlayerMovement.ButtonClickEvent -= DisplayEvent;
    }

    private void initializeSteps() {

        var sayDialog = GameObject.Find("DialogueBox").transform.Find("Panel").GetComponent<RectTransform>();
        var pos = sayDialog.localPosition;
        steps.Add(new TutorialStepInfo("Click on the blinking right arrow", "right",new Vector3(pos.x-30, pos.y-80, pos.z),RIGHT, NOROTATE));
        steps.Add(new TutorialStepInfo("Rotate the box now!", "clockwise", new Vector3(pos.x+100, pos.y, pos.z),NONE, CLOCKWISE));  
        steps.Add(new TutorialStepInfo("Move the box to match the pattern on the wall", "right", new Vector3(pos.x+250, pos.y+18, pos.z),RIGHT, NOROTATE));
    }

}

public class TutorialStepInfo {
    private string buttonName;
    private string message;

    private Vector3Int nextBtn;
    private Vector3 pos;
    private Vector3 nextRotateBtn;

    public TutorialStepInfo(string msg, string btn, Vector3 vectorPos, Vector3Int nxt, Vector3 nxtR) {
        buttonName = btn;
        message = msg; 
        pos = vectorPos;
        nextBtn = nxt;
        nextRotateBtn = nxtR;
    }

    public string getButtonName() {
        return buttonName;
    }

    public string getMessage() {
        return message;
    }

    public Vector3 getPanelPos() {
        return pos;
    }

    public Vector3Int getNextBtn() {
        return nextBtn;
    }

    public Vector3 getNextRBtn() {
        return nextRotateBtn;
    }
}
