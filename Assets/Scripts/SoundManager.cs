using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip levelCompleteSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        levelCompleteSound = Resources.Load<AudioClip>("clapping");

        audioSrc = GetComponent<AudioSource> ();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound () {

        audioSrc.PlayOneShot(levelCompleteSound);
        Debug.Log("In playSound \n");
    }
}
