using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // The main board that the camera rotates around
    public GameObject board;
    private const float ROTATE_SPEED = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Pressing A or LeftArrow -> Rotate the camera left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.RotateAround(board.transform.position, Vector3.up, ROTATE_SPEED * Time.deltaTime);
        }

        // Pressing D or RightArrow -> Rotate the camera right
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.RotateAround(board.transform.position, -Vector3.up, ROTATE_SPEED * Time.deltaTime);
        }
    }
}
