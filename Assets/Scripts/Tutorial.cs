using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        CameraControl.ClickEvent += Test;
        CameraControl.DeselectEvent += Deselect;
        playerMovement = GameObject.Find("GameManager").GetComponent<PlayerMovement>();
    }

    private void Test() {
        Debug.Log("CLICK EVENT ***************");
        playerMovement.BlinkBtn("right", "none");
    }

    private void Deselect() {

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
