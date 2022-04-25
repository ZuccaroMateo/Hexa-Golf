using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{   
    public AudioClip applause;
    // Start is called before the first frame update
    void Start()
    {
        GameSettings.instance.audioSource.PlayOneShot(applause);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
