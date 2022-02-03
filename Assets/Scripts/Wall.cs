using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wall : MonoBehaviour
{
    private GameObject[] boxes;
    private GameObject[] clones;
    private const string BOX_TAG = "Box";
    public Vector3 wallScale;
    private float OFFSET = 0.001f;

    // given a clone and box, modify the clone's transform so that it forms a shadow on this wall
    private void SetCloneShadowOnWall(GameObject clone, GameObject box)
    {
        //Vector3 wallUp = this.transform.up;
        Vector3 wallPosition = this.transform.position;

        Vector3 boxPosition = box.transform.position;
        float x = (boxPosition.x * wallScale.x) + wallPosition.x - Mathf.Sign(wallPosition.x) * OFFSET;
        float z = (boxPosition.z * wallScale.z) + wallPosition.z - Mathf.Sign(wallPosition.z) * OFFSET;
        clone.transform.position = new Vector3(x, clone.transform.position.y, z);
        clone.transform.GetChild(0).transform.rotation = box.transform.GetChild(0).transform.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 wallPosition = this.transform.position;
        boxes = GameObject.FindGameObjectsWithTag(BOX_TAG).Select(box => box.transform.parent.gameObject).ToArray();
        clones = new GameObject[boxes.Length];
        for (int i = 0; i < boxes.Length; ++i)
        {
            // Make a clone of the box
            GameObject box = boxes[i];
            GameObject clone = Instantiate(box);
            clones[i] = clone;
            Vector3 scale = box.transform.localScale;

            // "flatten" the clone on the wall
            clone.transform.localScale = new Vector3(scale.x * wallScale.x, scale.y * wallScale.y, scale.z * wallScale.z);

            // Set the transform and box rotation on the wall
            SetCloneShadowOnWall(clone, box);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // the shadow clones will follow their original boxes
        for (int i = 0; i < boxes.Length; ++i)
        {
            GameObject box = boxes[i];
            GameObject clone = clones[i];
            SetCloneShadowOnWall(clone, box);
        }
    }
}
