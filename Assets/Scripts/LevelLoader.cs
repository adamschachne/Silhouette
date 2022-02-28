using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [Scene]
    public string[] levels;

    void Awake()
    {
        // Load the first level
        SceneManager.LoadScene(levels[0], LoadSceneMode.Additive);
        
    }
}
