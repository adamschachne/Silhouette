using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightAnimation : MonoBehaviour
{
    private Animator animator;
    public GameObject wall;

    private Wall wallScript;

    void Awake()
    {
        animator = GetComponent<Animator>();
        wallScript = wall.GetComponent<Wall>();
    }

    private void Start()
    {
        animator.Play("Flashlight Up");
    }

    public void TurnOnShadows()
    {
        wallScript.ShowShadows();
    }

    public void TurnOffShadows()
    {
        wallScript.HideShadows();
    }
}
