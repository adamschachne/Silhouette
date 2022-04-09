using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionManager : MonoBehaviour
{
    public GameObject[] wallSolutions;
    public Material solutionCubeMaterial;
    public Material solutionCubeActiveMaterial;
    public Material invisibleBoxMat;

    public static readonly string redWallTag = "RedWall";
    public static readonly string blueWallTag = "BlueWall";

    private static Dictionary<CubeScript, int> solutionDict;
    private static Dictionary<CubeScript, int> nonSolutionDict;
    private LevelManager levelManager;

    // the target solution
    private static int targetSolution;
    private bool foundSolution;
    protected float Timer = 0f;

    public void CubeTriggerEnter(bool isSolutionCube, CubeScript cube)
    {
        if (isSolutionCube)
        {
            if (solutionDict.ContainsKey(cube) == false)
            {
                solutionDict.Add(cube, 0);
                cube.SetMaterial(solutionCubeActiveMaterial);
            }
            solutionDict[cube]++;
        }
        else
        {
            if (nonSolutionDict.ContainsKey(cube) == false)
            {
                nonSolutionDict.Add(cube, 0);
            }
            nonSolutionDict[cube]++;
        }
    }

    public void CubeTriggerExit(bool isSolutionCube, CubeScript cube)
    {
        if (isSolutionCube)
        {
            solutionDict[cube]--;
            if (solutionDict[cube] == 0)
            {
                solutionDict.Remove(cube);
                cube.SetMaterial(solutionCubeMaterial);
            }
        }
        else
        {
            nonSolutionDict[cube]--;
            if (nonSolutionDict[cube] == 0)
            {
                nonSolutionDict.Remove(cube);
            }
        }
    }

    public IEnumerator CheckSolution()
    {
        yield return new WaitForFixedUpdate();
        if (foundSolution == false && targetSolution == solutionDict.Keys.Count && nonSolutionDict.Keys.Count == 0)
        {
            Debug.Log("You Win!");
            foundSolution = true;
            levelManager.LoadVictoryScene();
        }
    }

    public static int GetCurrentNumberOfBoxes()
    {
        return solutionDict.Keys.Count;
    }

    public static int GetTargetSoultion()
    {
        return targetSolution;
    }

    void Awake()
    {
        solutionDict = new Dictionary<CubeScript, int>();
        nonSolutionDict = new Dictionary<CubeScript, int>();

        foundSolution = false;
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();

        targetSolution = 0;

        PlayerData.NumberOfSeconds = 0;
        PlayerData.LevelsStarted.Add(PlayerData.CurrentLevel);
        PlayerData.DegreesCameraRotated = 0f;
        PlayerData.NumberOfMoves = 0;
        PlayerData.NumberOfRotations = 0;

        Debug.Log("The current level: " + PlayerData.CurrentLevel);

        AnalyticsSender.SendLevelReachedEvent();

        foreach (GameObject wallSolution in wallSolutions)
        {
            for (int i = 0; i < wallSolution.transform.childCount; ++i)
            {
                GameObject cube = wallSolution.transform.GetChild(i).gameObject;

                cube.AddComponent<CubeScript>();
                cube.GetComponent<CubeScript>().manager = this;

                if (cube.CompareTag(CubeScript.SOLUTION_CUBE_TAG))
                {
                    // DEBUG
                    cube.GetComponent<MeshRenderer>().enabled = true;
                    targetSolution++;
                }
                else
                {
                    cube.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }

    private void Start()
    {
        PlayerMovement playerMovement = GameObject.Find("GameManager").GetComponent<PlayerMovement>();
        playerMovement.checkForSolution = () =>
        {
            StartCoroutine(CheckSolution());
        };
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 1)
        {
            Timer = 0f;
            PlayerData.NumberOfSeconds += 1;
        }
    }
}
