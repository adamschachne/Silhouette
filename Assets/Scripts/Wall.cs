using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wall : MonoBehaviour
{
    public static readonly string SHADOW_TAG = "Shadow";
    public Vector3 wallScale;
    public GameObject flashlight;
    public Material shadowMaterial;
    public const string redBoxTag = "RedBox";
    public const string blueBoxTag = "BlueBox";

    private GameObject[] polys;
    private Dictionary<GameObject, GameObject> cloneMap;
    private const string UNTAGGED_TAG = "Untagged";
    private const float OFFSET = 0.01f;
    public enum WallColor
    {
        Red,
        Blue,
        Default
    }

    // given a clone and box, modify the clone's transform so that it forms a shadow on this wall
    private void SetCloneShadowOnWall(GameObject poly)
    {
        GameObject clone = cloneMap[poly];
        Vector3 wallPosition = this.transform.position;
        clone.transform.position = poly.transform.position;
        clone.transform.rotation = poly.transform.rotation;

        foreach (Transform polyCube in poly.transform)
        {
            GameObject cloneCube;
            if (cloneMap.TryGetValue(polyCube.gameObject, out cloneCube) == false)
            {
                continue;
            }

            float x = (polyCube.position.x * wallScale.x) + wallPosition.x;
            float z = (polyCube.position.z * wallScale.z) + wallPosition.z;
            cloneCube.transform.position = new Vector3(x, cloneCube.transform.position.y, z);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cloneMap = new Dictionary<GameObject, GameObject>();

        Dictionary<string, WallColor> tagToColor = new Dictionary<string, WallColor>()
        {
            [SolutionManager.redWallTag] = WallColor.Red,
            [SolutionManager.blueWallTag] = WallColor.Blue,
            [redBoxTag] = WallColor.Red,
            [blueBoxTag] = WallColor.Blue,
        };

        var defaultLayer = LayerMask.NameToLayer("Default");
        var solutionManager = GameObject.Find("SolutionManager").GetComponent<SolutionManager>();

        WallColor wallColor = WallColor.Default;
        string wallTag = solutionManager.wallSolutions.First(go => go.name.StartsWith(this.name))?.tag ?? "";
        if (tagToColor.ContainsKey(wallTag))
        {
            wallColor = tagToColor[wallTag];
        }

        Vector3 wallPosition = this.transform.position;
        polys = GameObject.FindGameObjectsWithTag(PlayerMovement.POLY_TAG);

        for (int i = 0; i < polys.Length; ++i)
        {
            // Make a clone of the box
            GameObject poly = polys[i];
            GameObject polyClone = Instantiate(poly);
            GameObject shadow = new GameObject();

            cloneMap[poly] = polyClone;

            shadow.name = $"{poly.name} shadow on {this.name}";
            polyClone.transform.SetParent(shadow.transform);

            // give the shadow some thickness greater than 0 to avoid weird graphics bugs
            shadow.transform.localScale = new Vector3(Mathf.Max(OFFSET, wallScale.x), Mathf.Max(OFFSET, wallScale.y), Mathf.Max(OFFSET, wallScale.z));
            shadow.transform.position = new Vector3(wallPosition.x, 0, wallPosition.z);
            
            // change tag from "poly" to "untagged"
            polyClone.tag = UNTAGGED_TAG;

            // change each cube's tag inside the poly to SHADOW_TAG to prevent selection/deselection
            // also change material to shadow material
            for (int j = 0; j < polyClone.transform.childCount; ++j) {
                Transform polyCube = poly.transform.GetChild(j);
                var cubeClone = polyClone.transform.GetChild(j);
                var cubeColor = WallColor.Default;

                // Handle colored boxes
                var childCube = cubeClone.childCount > 0 ? cubeClone.GetChild(0) : null;
                if (childCube)
                {
                    // determine the color of this cube
                    cubeColor = tagToColor.TryGetValue(childCube.tag, out cubeColor) ? cubeColor : WallColor.Default;

                    if (cubeColor == wallColor)
                    {
                        // Don't make a shadow for this cube if it is the same color as the light
                        Destroy(cubeClone.gameObject);
                        continue;
                    }
                    else
                    {
                        // the shadow clone doesn't need the extra nested cube
                        Destroy(childCube.gameObject);
                    }
                }

                cloneMap[polyCube.gameObject] = cubeClone.gameObject;

                cubeClone.tag = SHADOW_TAG;
                cubeClone.gameObject.layer = defaultLayer;
                cubeClone.gameObject.GetComponent<MeshRenderer>().material = shadowMaterial;                
            }
         
            // Set the transform and box rotation on the wall
            SetCloneShadowOnWall(poly);

            flashlight.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // the shadow clones will follow their original boxes
        foreach (GameObject poly in polys)
        {
            try
            {
                SetCloneShadowOnWall(poly);
            } 
            catch (System.Exception e)
            {
                Debug.Log("error" + e.Message);
            }
            
        }
    }
}
