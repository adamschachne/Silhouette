using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CameraControl.ClickEvent += Test;
    }

    private void Test() {
        Debug.Log("CLICK EVENT ***************");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onDisable() {
        CameraControl.ClickEvent -= Test;
    }

}
