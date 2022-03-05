using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // The main board that the camera rotates around
    public GameObject board;
    public GameObject gameManager;
    public GameObject arrowKeys;
    private const float ROTATE_SPEED = 100.0f;

     private const float ROTATE_SPEED_DRAG = 50.0f;
    private const float MAX_RAYCAST_DIST = 1000f;
    private const string BOX_TAG = "Box";
    private const string POLY_TAG = "Poly";
    public float angleMax = 43.0f;

    // private float sensitivity = 0.5f; 

    private Vector3 dragOrigin;
     private Vector3 initialVector = Vector3.forward;
    private void DeselectAllPolys()
    {
        GameObject[] polys = GameObject.FindGameObjectsWithTag(POLY_TAG);
        foreach (GameObject poly in polys)
        {
            poly.GetComponent<Outline>().enabled = false;
        }
        gameManager.GetComponent<PlayerMovement>().selectedPoly = null;
    }
    private void SelectPoly(GameObject poly)
    {
        DeselectAllPolys();
        Outline outline = poly.GetComponent<Outline>();
        outline.enabled = true;
        gameManager.GetComponent<PlayerMovement>().selectedPoly = poly;
    }

    // Start is called before the first frame update
    void Start()
    {
         if(board.transform != null)
         {
             initialVector = transform.position - board.transform.position;
             initialVector.y = 0;
         }        
    }

    // Update is called once per frame
    void Update()
    {

        // Pressing mouse 1 AND not pressing the buttons
        if (Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current != null &&
            !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit[] hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hits = Physics.RaycastAll(ray, MAX_RAYCAST_DIST).Where(hit => hit.transform.gameObject.CompareTag(BOX_TAG)).ToArray();
            if (hits.Length > 0) // Hit a "Box" -> Select it
            {
                System.Array.Sort(hits, (hit1, hit2) => hit1.distance < hit2.distance ? -1 : 1);
                SelectPoly(hits[0].transform.parent.gameObject);
            }
            else // Clicked on nothing -> Deselect selected box
            {
                DeselectAllPolys();
            }
            dragOrigin = Input.mousePosition;
            return;
        }

        float rotateDegrees = 0f;
        bool keyPressed = false;
        // Pressing A or LeftArrow -> Rotate the camera left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rotateDegrees += ROTATE_SPEED * Time.deltaTime;
            keyPressed = true;

 
            // this.transform.RotateAround(board.transform.position, Vector3.up, ROTATE_SPEED * Time.deltaTime);
            // arrowKeys.transform.RotateAround(arrowKeys.transform.position, Vector3.forward, ROTATE_SPEED * Time.deltaTime);
        }

        // Pressing D or RightArrow -> Rotate the camera right
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rotateDegrees -= ROTATE_SPEED * Time.deltaTime;
            keyPressed = true;
        }

       // Left click and drag to rotate the camera 
        else if (Input.GetMouseButton(0) && UnityEngine.EventSystems.EventSystem.current != null &&
            !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() ) {

            // if (Application.platform == RuntimePlatform.WebGLPlayer) {
                    
            //         // Debug.Log("Inside WEBGL");    
            //         sensitivity = 1.0f / 75;
            // } else {
                    
            //         sensitivity = 0.5f;
            //         // Debug.Log("Inside NON WEBGL");
            // }
            // rotateDegrees = ROTATE_SPEED_DRAG * Input.GetAxis("Mouse X") * sensitivity;
            // keyPressed = true;
            //------------------------------------------------------------------------------
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            rotateDegrees = ROTATE_SPEED_DRAG * pos.x *0.1f;
            Debug.Log(rotateDegrees); 
            keyPressed = true;
        }

        // Rotate the camera if key is pressed or long press and drag left click
        if(keyPressed) {
            //this.transform.RotateAround(board.transform.position, -Vector3.up, ROTATE_SPEED * Time.deltaTime);
            if(board.transform != null) {

                Vector3 currentVector = transform.position - board.transform.position;
                currentVector.y = 0;
                float angleBetween = Vector3.Angle(initialVector, currentVector) * (Vector3.Cross(initialVector, currentVector).y > 0 ? 1 : -1);            
                float newAngle = Mathf.Clamp(angleBetween + rotateDegrees, -angleMax, angleMax);
                rotateDegrees = newAngle - angleBetween;
                this.transform.RotateAround(board.transform.position, Vector3.up, rotateDegrees);
                arrowKeys.transform.RotateAround(arrowKeys.transform.position, Vector3.forward, rotateDegrees);
            }
        }

    }

}
