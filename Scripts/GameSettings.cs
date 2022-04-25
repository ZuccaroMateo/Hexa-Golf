using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameSettings : MonoBehaviour
{   

    public static GameSettings instance;

    public AudioClip playSong, menuSong;
    public AudioSource audioSource;

    void Awake() {
        //DestroyOnLoad(this.gameObject);
        if (GameSettings.instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}else {
			Destroy(gameObject);
            return;
		}
    }
    void Start()
    {   
        audioSource = GetComponent<AudioSource>();   
    }

    public static void ChangeMusic()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            instance.audioSource.clip = instance.menuSong;
        else
            instance.audioSource.clip = instance.playSong;

        instance.audioSource.Play();

    }

    public static void changeVolume(Slider volumeSlider){
        instance.audioSource.volume = volumeSlider.value;
    }


    public static void changeHolesPerRoundText(GameObject text){
        
    }
}
