using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public SolutionManager manager;
    public static readonly string SOLUTION_CUBE_TAG = "SolutionCube";
    private bool isSolutionCube = false;
    private MeshRenderer cubeMesh;

    private void Start()
    {
        isSolutionCube = this.gameObject.CompareTag(SOLUTION_CUBE_TAG) == true;
        cubeMesh = GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        manager.CubeTriggerEnter(isSolutionCube, this);
    }

    void OnTriggerExit(Collider other)
    {
        manager.CubeTriggerExit(isSolutionCube, this);
    }

    public void SetMaterial(Material mat)
    {
        cubeMesh.material = mat;
    }
}