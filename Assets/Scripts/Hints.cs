using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour
{
    public bool isTutorial;
    public GameObject[] actualBoxes;
    public GameObject[] hintBoxes;
    private bool[] used;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] hintUIGameObjects = GameObject.FindGameObjectsWithTag("HintUI");
        if (isTutorial && hintUIGameObjects.Length != 0)
        {
            foreach (GameObject hintUi in hintUIGameObjects)
            {
                hintUi.SetActive(false);
            }
        }

        used = new bool[hintBoxes.Length];

        for (int i = 0; i < hintBoxes.Length; i++)
        {
            used[i] = false;

            // in tutorials, always show the hints
            MeshRenderer[] boxRenderers = hintBoxes[i].GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer r in boxRenderers)
            {
                if (!isTutorial)
                {
                    r.enabled = false;
                }
            }
        }
    }

    public bool ShowAHint()
    {
        for (int i = 0; i < hintBoxes.Length; i++)
        {
            // Do NOT use this hint when the actual box overlaps with the corresponding hint box
            if (actualBoxes[i].transform.position == hintBoxes[i].transform.position &&
                actualBoxes[i].transform.rotation == hintBoxes[i].transform.rotation)
            {
                continue;
            }

            if (used[i] == false)
            {

                MeshRenderer[] boxRenderers = hintBoxes[i].GetComponentsInChildren<MeshRenderer>();

                foreach (MeshRenderer r in boxRenderers)
                {
                    if (!r.enabled)
                    {
                        r.enabled = true;
                        used[i] = true;
                    }
                }
                return true;
            }
        }

        return false;
    }
}
