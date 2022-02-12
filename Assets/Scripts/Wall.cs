using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wall : MonoBehaviour
{
    private GameObject[] polys;
    private GameObject[] clones;
    private const string POLY_TAG = "Poly";
    public Vector3 wallScale;
    public Vector3 otherWallScale;
    private float OFFSET = 0.001f;

    // given a clone and box, modify the clone's transform so that it forms a shadow on this wall
    private void SetCloneShadowOnWall(GameObject clone, GameObject poly)
    {
        //Vector3 wallUp = this.transform.up;
        Vector3 wallPosition = this.transform.position;

        Vector3 polyPosition = poly.transform.position;
        Vector3 scale = poly.transform.localScale;
        float x = (polyPosition.x * wallScale.x) + wallPosition.x - Mathf.Sign(wallPosition.x);
        float z = (polyPosition.z * wallScale.z) + wallPosition.z - Mathf.Sign(wallPosition.z);
        clone.transform.position = new Vector3(x, clone.transform.position.y, z);
        clone.transform.rotation = poly.transform.rotation;

        // "flatten" the clone on the wall
        if (clone.transform.eulerAngles.y % 180 == 0)
        {
            clone.transform.localScale = new Vector3(wallScale.x * scale.x, wallScale.y * scale.y, wallScale.z * scale.z);
        }
        else
        {
            clone.transform.localScale = new Vector3(otherWallScale.x * scale.x, otherWallScale.y * scale.y, otherWallScale.z * scale.z);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 wallPosition = this.transform.position;
        polys = GameObject.FindGameObjectsWithTag(POLY_TAG);
        clones = new GameObject[polys.Length];
        for (int i = 0; i < polys.Length; ++i)
        {
            // Make a clone of the box
            GameObject poly = polys[i];
            GameObject clone = Instantiate(poly);
            clones[i] = clone;
            Vector3 scale = poly.transform.localScale;
         
            // Set the transform and box rotation on the wall
            SetCloneShadowOnWall(clone, poly);
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
