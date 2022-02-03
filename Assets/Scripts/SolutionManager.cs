using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public SolutionManager manager;
    public static readonly string SOLUTION_CUBE_TAG = "SolutionCube";
    private bool isSolutionCube = false;

    private void Start()
    {
        isSolutionCube = this.gameObject.CompareTag(SOLUTION_CUBE_TAG) == true;
    }

    void OnTriggerEnter(Collider other)
    {
        manager.CubeTriggerEnter(isSolutionCube, this);
    }

    void OnTriggerExit(Collider other)
    {
        manager.CubeTriggerExit(isSolutionCube, this);
    }
}

public class SolutionManager : MonoBehaviour
{
    public GameObject[] wallSolutions;
    private Dictionary<CubeScript, int> solutionDict = new Dictionary<CubeScript, int>();
    private Dictionary<CubeScript, int> nonSolutionDict = new Dictionary<CubeScript, int>();

    // the target solution
    private int targetSolution = 0;

    public void CubeTriggerEnter(bool isSolutionCube, CubeScript cube)
    {
        if (isSolutionCube)
        {
            if (solutionDict.ContainsKey(cube) == false)
            {
                solutionDict.Add(cube, 0);
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

        CheckSolution();
    }
    public void CubeTriggerExit(bool isSolutionCube, CubeScript cube)
    {
        if (isSolutionCube)
        {
            solutionDict[cube]--;
            if (solutionDict[cube] == 0)
            {
                solutionDict.Remove(cube);
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

        CheckSolution();
    }

    private void CheckSolution()
    {
        if (targetSolution == solutionDict.Keys.Count && nonSolutionDict.Keys.Count == 0)
        {
            Debug.Log("You Win!");
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        foreach (GameObject wallSolution in wallSolutions)
        {
            for (int i = 0; i < wallSolution.transform.childCount; ++i)
            {
                GameObject cube = wallSolution.transform.GetChild(i).gameObject;
                BoxCollider collider = cube.GetComponent<BoxCollider>();

                cube.AddComponent<CubeScript>();
                cube.GetComponent<CubeScript>().manager = this;

                if (cube.CompareTag(CubeScript.SOLUTION_CUBE_TAG))
                {
                    // DEBUG
                    cube.GetComponent<MeshRenderer>().enabled = true;

                    targetSolution++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
