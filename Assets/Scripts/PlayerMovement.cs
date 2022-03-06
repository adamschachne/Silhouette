using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private GameObject selectedPoly = null;
    public Tilemap tileMap = null;
    public const float timeToMove = 0.2f;
    public const int gridSize = 10;
    public const float spinSpeed = 20;

    private bool isMoving = false;
    private Vector3 oldPos;
    private Vector3 targetPos;

    private bool isRotating = false;

    private Vector3Int UP = new Vector3Int(1, 0, 0);
    private Vector3Int DOWN = new Vector3Int(-1, 0, 0);
    private Vector3Int LEFT = new Vector3Int(0, 0, 1);
    private Vector3Int RIGHT = new Vector3Int(0, 0, -1);

    private Vector3 CLOCKWISE = 90 * Vector3.up;
    private Vector3 COUNTERCLOCKWISE = -90 * Vector3.up;

    public Button moveUpButton;
    public Button moveDownButton;
    public Button moveLeftButton;
    public Button moveRightButton;
    public Button rotateClockwiseButton;
    public Button rotateCounterclockwiseButton;

    public GameObject SelectedPoly
    {
        get
        {
            return selectedPoly;
        }

        set
        {
            selectedPoly = value;
            CheckPossibleMoves();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckPossibleMoves();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /******* Move *******/

    // Computed once after a move or rotate
    // "Tests" which moves are possible and sets the interactive attribute on each respective button
    private void CheckPossibleMoves()
    {
        moveUpButton.interactable = CanMove(UP);
        moveDownButton.interactable = CanMove(DOWN);
        moveLeftButton.interactable = CanMove(UP);
        moveRightButton.interactable = CanMove(UP);
        rotateClockwiseButton.interactable = CanRotate(CLOCKWISE);
        rotateCounterclockwiseButton.interactable = CanRotate(COUNTERCLOCKWISE);
    }

    public void MoveBoxUp() //positive x
    {
        if (!isMoving && selectedPoly != null)
        {
            StartCoroutine(MoveBox(UP));
        }
    }

    public void MoveBoxDown() //negative x
    {
        if (!isMoving && selectedPoly != null)
        {
            StartCoroutine(MoveBox(DOWN));
        }
    }

    public void MoveBoxLeft() //positive z
    {
        if (!isMoving && selectedPoly != null)
        {
            StartCoroutine(MoveBox(LEFT));
        }
    }

    public void MoveBoxRight() //negative z
    {
        if (!isMoving && selectedPoly != null)
        {
            StartCoroutine(MoveBox(RIGHT));
        }
    }

    private bool CanMove(Vector3Int dir)
    {
        return true;
    }

    private IEnumerator MoveBox(Vector3Int dir)
    {
        isMoving = true;

        float elapsedTime = 0;

        oldPos = selectedPoly.transform.position;
        targetPos = oldPos + dir * gridSize;

        while (elapsedTime < timeToMove)
        {
            selectedPoly.transform.position = Vector3.Lerp(oldPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // the final position should be exactly targetPos at the end of the animation
        selectedPoly.transform.position = targetPos;

        isMoving = false;
    }

    /******* Rotate *******/
    public void ClockwiseRotate()
    {
        if (!isRotating && selectedPoly != null)
        {
            StartCoroutine(RotateBox(CLOCKWISE));
        }
    }

    public void CounterClockwiseRotate()
    {
        if (!isRotating && selectedPoly != null)
        {
            StartCoroutine(RotateBox(COUNTERCLOCKWISE));
        }
    }

    private bool CanRotate(Vector3 dir)
    {
        return true;
    }

    private IEnumerator RotateBox(Vector3 dir)
    {
        isRotating = true;

        float elapsedTime = 0;

        Quaternion startRotation = selectedPoly.transform.rotation;
        Quaternion targetRotation = selectedPoly.transform.rotation * Quaternion.Euler(dir);
        while (elapsedTime < timeToMove)
        {
            selectedPoly.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        selectedPoly.transform.rotation = targetRotation;

        isRotating = false;
    }
}
