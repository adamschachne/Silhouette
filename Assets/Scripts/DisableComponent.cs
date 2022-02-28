using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableComponent : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.SetActive(false);
    }
}
