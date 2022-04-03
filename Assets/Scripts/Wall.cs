using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wall : MonoBehaviour
{
    public static readonly string SHADOW_TAG = "Shadow";
    public Vector3 wallScale;
    public Vector3 thickness;
    public GameObject flashlight;

    private GameObject[] polys;
    private GameObject[] clones;
    private const string UNTAGGED_TAG = "Untagged";
    private const float OFFSET = 0.01f;

    // given a clone and box, modify the clone's transform so that it forms a shadow on this wall
    private void SetCloneShadowOnWall(GameObject clone, GameObject poly)
    {
        Vector3 wallPosition = this.transform.position;
        clone.transform.position = poly.transform.position;
        clone.transform.rotation = poly.transform.rotation;

        for (int i = 0; i < poly.transform.childCount; ++i)
        {
            var polyCube = poly.transform.GetChild(i);
            var cloneCube = clone.transform.GetChild(i);
            float x = (polyCube.position.x * wallScale.x) + wallPosition.x;
            float z = (polyCube.position.z * wallScale.z) + wallPosition.z;
            cloneCube.transform.position = new Vector3(x, cloneCube.position.y, z);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 wallPosition = this.transform.position;
        polys = GameObject.FindGameObjectsWithTag(PlayerMovement.POLY_TAG);
        clones = new GameObject[polys.Length];
        for (int i = 0; i < polys.Length; ++i)
        {
            // Make a clone of the box
            GameObject poly = polys[i];
            GameObject clone = Instantiate(poly);
            GameObject shadow = new GameObject();
            shadow.name = $"{poly.name} shadow on {this.name}";
            clone.transform.SetParent(shadow.transform);

            // give the shadow some thickness greater than 0 to avoid weird graphics bugs
            shadow.transform.localScale = new Vector3(Mathf.Max(OFFSET, wallScale.x), Mathf.Max(OFFSET, wallScale.y), Mathf.Max(OFFSET, wallScale.z));
            shadow.transform.position = new Vector3(wallPosition.x, 0, wallPosition.z);

            clone.tag = UNTAGGED_TAG;
            
            // remove the Box tag from these cubes to prevent selecting the shadow
            for (int j = 0; j < clone.transform.childCount; ++j) {
                clone.transform.GetChild(j).transform.tag = SHADOW_TAG;
            }

            clones[i] = clone;
         
            // Set the transform and box rotation on the wall
            SetCloneShadowOnWall(clone, poly);

            flashlight.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // the shadow clones will follow their original boxes
        for (int i = 0; i < polys.Length; ++i)
        {
            GameObject poly = polys[i];
            GameObject clone = clones[i];
            SetCloneShadowOnWall(clone, poly);
        }
    }
}
