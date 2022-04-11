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
    private const float OFFSET = 0.001f;

    // given a clone and box, modify the clone's transform so that it forms a shadow on this wall
    private void SetCloneShadowOnWall(GameObject clone, GameObject poly)
    {
        Vector3 wallPosition = this.transform.position;
        Vector3 polyPosition = poly.transform.position;
        float x = (polyPosition.x * wallScale.x) + wallPosition.x;
        float z = (polyPosition.z * wallScale.z) + wallPosition.z;
        clone.transform.position = new Vector3(x, clone.transform.position.y, z);
        clone.transform.rotation = poly.transform.rotation;
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
            shadow.transform.localScale = wallScale;
            shadow.transform.position = new Vector3(wallPosition.x - (1 - wallScale.x) * OFFSET, 0, wallPosition.z - (1 - wallScale.z) * OFFSET);

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
