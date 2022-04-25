using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MainMenu : MonoBehaviour
{      

    public TMP_Text holeText;

    void Start() {
        GameSettings.ChangeMusic();
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void changeVolume(Slider slider)
    {
        GameSettings.changeVolume(slider);
    }

    public void changeHolesPerRound(Slider holesSlider)
    {
        RoundData.instance.holesPerRound = (int) holesSlider.value;
        holeText.GetComponent<TMP_Text>().text = "holes per round: " + RoundData.instance.holesPerRound;
    }


}
