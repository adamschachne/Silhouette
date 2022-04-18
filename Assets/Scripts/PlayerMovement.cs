using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public static readonly string POLY_TAG = "Poly";
    public static readonly string GHOST_POLY_TAG = "GhostPoly";
    public static readonly string GHOST_BOX_TAG = "GhostBox";

    private GameObject selectedPoly = null;
    public Tilemap tileMap = null;
    public const float timeToMove = 0.2f;
    public const int gridSize = 10;
    public const float spinSpeed = 20;
    private const string UP_STRING = "Up";
    private const string LEFT_STRING = "Left";
    private const string RIGHT_STRING = "Right";
    private const string DOWN_STRING = "Down";
    private const string COUNTER_CLOCKWISE_STRING = "CounterClockwise";
    private const string CLOCKWISE_STRING = "Clockwise";
    private Queue<string> moves = new Queue<string>();
    private bool isMoving = false;
    private Vector3 oldPos;
    private Vector3 targetPos;


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
    public TextMeshProUGUI hintsCountText;

    private Dictionary<int, GameObject> polyToGhostMap;
    private float timeBetweenMoves = 0;

    public static int numHints = 3;
    private GameObject solutionManager;

    public System.Action checkForSolution;

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
        polyToGhostMap = new Dictionary<int, GameObject>();
        var polys = GameObject.FindGameObjectsWithTag(POLY_TAG);
        foreach (var poly in polys)
        {
            var ghostPoly = Instantiate(poly);
            ghostPoly.tag = GHOST_POLY_TAG;
            ghostPoly.name = $"Ghost {poly.name}";

            for (int i = 0; i < ghostPoly.transform.childCount; ++i)
            {
                GameObject child = ghostPoly.transform.GetChild(i).gameObject;
                child.tag = GHOST_BOX_TAG;
                child.transform.localScale = child.transform.localScale * 0.5f; // halve the cube's scale to avoid edge collisions
                Renderer r = child.GetComponent<Renderer>();
                r.enabled = false;
            }

            foreach (Renderer r in ghostPoly.GetComponentsInChildren(typeof(Renderer)))
            {
                r.enabled = false;
            }

            polyToGhostMap.Add(poly.GetInstanceID(), ghostPoly);
        }
        CheckPossibleMoves();

        solutionManager = GameObject.Find("SolutionManager");
    }


    private void Update()
    {
        timeBetweenMoves += Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            moves.Enqueue(UP_STRING);

        }
        if (Input.GetKey(KeyCode.A))
        {
            moves.Enqueue(LEFT_STRING);

        }
        if (Input.GetKey(KeyCode.S))
        {
            moves.Enqueue(DOWN_STRING);

        }
        if (Input.GetKey(KeyCode.D))
        {
            moves.Enqueue(RIGHT_STRING);

        }

        if (Input.GetKey(KeyCode.E))
        {
            moves.Enqueue(COUNTER_CLOCKWISE_STRING);

        }
        if (Input.GetKey(KeyCode.R))
        {
            moves.Enqueue(CLOCKWISE_STRING);

        }

        string move;
        while (moves.Count > 0)
        {
            move = moves.Dequeue();
            switch (move)
            {
                case UP_STRING:
                    if (CanMove(UP))
                    {
                        MoveBoxUp();
                    }
                    break;
                case LEFT_STRING:
                    if (CanMove(LEFT))
                    {
                        MoveBoxLeft();
                    }
                    break;
                case RIGHT_STRING:
                    if (CanMove(RIGHT))
                    {
                        MoveBoxRight();
                    }
                    break;
                case DOWN_STRING:
                    if (CanMove(DOWN))
                    {
                        MoveBoxDown();
                    }
                    break;
                case COUNTER_CLOCKWISE_STRING:
                    if (CanRotate(COUNTERCLOCKWISE))
                    {
                        CounterClockwiseRotate();
                    }
                    break;
                case CLOCKWISE_STRING:
                    if (CanRotate(CLOCKWISE))
                    {
                        ClockwiseRotate();
                    }
                    break;
            }
        }



        hintsCountText.text = "Hints Left: " + numHints;
    }

    
    /******* Move *******/

    // Computed once after a move or rotate
    // "Tests" which moves are possible and sets the interactive attribute on each respective button
    private void CheckPossibleMoves()
    {
        moveUpButton.interactable = CanMove(UP);
        moveDownButton.interactable = CanMove(DOWN);
        moveLeftButton.interactable = CanMove(LEFT);
        moveRightButton.interactable = CanMove(RIGHT);
        rotateClockwiseButton.interactable = CanRotate(CLOCKWISE);
        rotateCounterclockwiseButton.interactable = CanRotate(COUNTERCLOCKWISE);
    }

    public void MoveBoxUp() //positive x
    {
        if (!isMoving && selectedPoly != null)
        {
            AnalyticsSender.SendTimeBetweenMovesEvent(Mathf.RoundToInt(timeBetweenMoves));
            timeBetweenMoves = 0;
            StartCoroutine(MoveBox(UP));
        }
    }

    public void MoveBoxDown() //negative x
    {
        if (!isMoving && selectedPoly != null)
        {
            AnalyticsSender.SendTimeBetweenMovesEvent(Mathf.RoundToInt(timeBetweenMoves));
            timeBetweenMoves = 0;
            StartCoroutine(MoveBox(DOWN));
        }
    }

    public void MoveBoxLeft() //positive z
    {
        if (!isMoving && selectedPoly != null)
        {
            AnalyticsSender.SendTimeBetweenMovesEvent(Mathf.RoundToInt(timeBetweenMoves));
            timeBetweenMoves = 0;
            StartCoroutine(MoveBox(LEFT));
        }
    }

    public void MoveBoxRight() //negative z
    {
        if (!isMoving && selectedPoly != null)
        {
            AnalyticsSender.SendTimeBetweenMovesEvent(Mathf.RoundToInt(timeBetweenMoves));
            timeBetweenMoves = 0;
            StartCoroutine(MoveBox(RIGHT));
        }
    }

    private IEnumerator MoveBox(Vector3Int dir)
    {
        isMoving = true;
        PlayerData.NumberOfMoves += 1;

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
        CheckPossibleMoves();
        checkForSolution?.Invoke();
    }

    /******* Rotate *******/

    private IEnumerator RotateBox(Vector3 dir)
    {
        isMoving = true;
        PlayerData.NumberOfRotations += 1;

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
        isMoving = false;
        CheckPossibleMoves();
        checkForSolution?.Invoke();
    }


    public void ClockwiseRotate()
    {
        if (!isMoving && selectedPoly != null)
        {
            AnalyticsSender.SendTimeBetweenMovesEvent(Mathf.RoundToInt(timeBetweenMoves));
            timeBetweenMoves = 0;
            StartCoroutine(RotateBox(CLOCKWISE));
        }
    }

    public void CounterClockwiseRotate()
    {
        if (!isMoving && selectedPoly != null)
        {
            AnalyticsSender.SendTimeBetweenMovesEvent(Mathf.RoundToInt(timeBetweenMoves));
            timeBetweenMoves = 0;
            StartCoroutine(RotateBox(COUNTERCLOCKWISE));
        }
    }

    /******* Collision *******/

    // tests if the given Box transform is colliding with something
    // true if colliding with a wall or another box
    private bool IsCubeColliding(Transform ghostTransform)
    {
        GameObject ghostPoly = ghostTransform.gameObject;

        for (int i = 0; i < ghostTransform.childCount; ++i)
        {
            GameObject ghostBox = ghostTransform.GetChild(i).gameObject;
            var hits = Physics.BoxCastAll(ghostBox.transform.position, Vector3.one, ghostBox.transform.forward, ghostBox.transform.rotation, 0);
            foreach (var hit in hits)
            {
                GameObject hitParent = hit.transform?.parent?.gameObject ?? null;

                // ignore colliding with itself, a ghost cube, or the shadows
                if (hit.transform.gameObject.CompareTag(Wall.SHADOW_TAG) || hitParent == selectedPoly || hit.transform.gameObject.CompareTag(GHOST_BOX_TAG))
                {
                    continue;
                }

                return true;
            }
        }
        return false;
    }

    private bool CanMove(Vector3Int dir)
    {
        if (selectedPoly == null)
        {
            return false;
        }

        int instanceID = selectedPoly.GetInstanceID();
        var ghostPoly = polyToGhostMap[instanceID];
        ghostPoly.transform.position = selectedPoly.transform.position;
        ghostPoly.transform.rotation = selectedPoly.transform.rotation;

        // apply the target position to the poly and test for collisions
        var targetPos = selectedPoly.transform.position + dir * gridSize;
        ghostPoly.transform.position = targetPos;

        return !IsCubeColliding(ghostPoly.transform);
    }

    private bool CanRotate(Vector3 dir)
    {
        if (selectedPoly == null)
        {
            return false;
        }

        int instanceID = selectedPoly.GetInstanceID();
        var ghostPoly = polyToGhostMap[instanceID];
        ghostPoly.transform.position = selectedPoly.transform.position;
        ghostPoly.transform.rotation = selectedPoly.transform.rotation;

        // apply the target rotation to the poly and test for collisions
        Quaternion targetRotation = selectedPoly.transform.rotation * Quaternion.Euler(dir);
        ghostPoly.transform.rotation = targetRotation;

        return !IsCubeColliding(ghostPoly.transform);
    }


    /******* Hints *******/
    public void UseAHint()
    {
        if (numHints > 0 && solutionManager.GetComponent<Hints>().ShowAHint())
        {
            numHints -= 1;
        }

    }

    public void IncHintsCount()
    {
        numHints++;
    }
}
