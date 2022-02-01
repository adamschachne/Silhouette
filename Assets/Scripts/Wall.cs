using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private GameObject[] boxes;
    private GameObject[] clones;
    private const string BOX_TAG = "Box";
    public Vector3 wallScale;
    private float OFFSET = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 wallPosition = this.transform.position;
        boxes = GameObject.FindGameObjectsWithTag(BOX_TAG);
        clones = new GameObject[boxes.Length];
        for (int i = 0; i < boxes.Length; ++i)
        {
            // Make a clone of the box
            GameObject box = boxes[i];
            GameObject clone = GameObject.Instantiate(box);
            clones[i] = clone;
            Vector3 scale = box.transform.localScale;

            // "flatten" the clone and then move it to the wall
            clone.transform.localScale = new Vector3(scale.x * wallScale.x, scale.y * wallScale.y, scale.z * wallScale.z);
            float x = (clone.transform.position.x * wallScale.x) + wallPosition.x - Mathf.Sign(wallPosition.x) * OFFSET;
            float z = (clone.transform.position.z * wallScale.z) + wallPosition.z - Mathf.Sign(wallPosition.z)*OFFSET;
            clone.transform.position = new Vector3(x, clone.transform.position.y, z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 wallPosition = this.transform.position;
        // the shadow clones will follow their original boxes
        for (int i = 0; i < boxes.Length; ++i)
        {
            GameObject box = boxes[i];
            GameObject clone = clones[i];
            Vector3 boxPosition = box.transform.position;
            float x = (boxPosition.x * wallScale.x) + wallPosition.x - Mathf.Sign(wallPosition.x) * OFFSET;
            float z = (boxPosition.z * wallScale.z) + wallPosition.z - Mathf.Sign(wallPosition.z) * OFFSET;
            clone.transform.position = new Vector3(x, clone.transform.position.y, z);
        }
    }
}
