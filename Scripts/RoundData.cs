using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HoleDataNamespace;

public class RoundData : MonoBehaviour
{   

    public AudioClip end_sound;
    public static RoundData instance;

    public int score, holesPerRound, currentHole; 

    public static HoleData[] scorecard = new HoleData[9];

    private int arrayIndex = 0;

    void Awake() {

		if (RoundData.instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}else {
            

			Destroy(gameObject);

            

            return;
		}

        if (SceneManager.GetActiveScene().buildIndex == 0) {

            
            resetStats();
        }

        
	}

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void resetStats()
    {
        instance.score = 0;
        instance.currentHole = 1;

        instance.holesPerRound = 3;

        RoundData.scorecard = new HoleData[9];
        instance.arrayIndex = 0;

    }

    public void addHole(int new_score, int new_par) 
    {   
        currentHole++;
        HoleData newHole = new HoleData(new_par, Mathf.Min(new_score, new_par + 3)); 
        RoundData.scorecard[arrayIndex] =  newHole; 

        score+= new_score - new_par;
        arrayIndex++;

        if (SceneManager.GetActiveScene().buildIndex == 2)
                GameSettings.instance.audioSource.PlayOneShot(end_sound);
    }

}
