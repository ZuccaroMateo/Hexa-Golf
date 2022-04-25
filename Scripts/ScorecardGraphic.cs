using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HoleDataNamespace;
using UnityEngine.UI;
public class ScorecardGraphic : MonoBehaviour
{   
    private GameObject scoreScroll, holeScroll;

    public GameObject score_pf, number_pf;

    // Start is called before the first frame update
    void Start()
    {

        scoreScroll = transform.GetChild(0).gameObject;
        holeScroll = transform.GetChild(1).gameObject;


        if (RoundData.instance.score > 0){
            GameObject.Find("finalScore").GetComponent<TMP_Text>().text = "final score\n" + "+" + RoundData.instance.score;
        }else if (RoundData.instance.score < 0){
            GameObject.Find("finalScore").GetComponent<TMP_Text>().text = "final score\n" + RoundData.instance.score;
        }
        else{
            GameObject.Find("finalScore").GetComponent<TMP_Text>().text = "final score\n" + "E";
        }

        GameObject.Find("MenuButton").GetComponent<Button>().onClick.AddListener(delegate { GameManager.backToMenu(); });

        setScoreCard();
    }

    // Update is called once per frame
    void setScoreCard()
    {  
        HoleData[] scorecard = RoundData.scorecard;

        Transform number_panel = holeScroll.transform.GetChild(0);
        Transform score_panel = scoreScroll.transform.GetChild(0);

        for (int i = 0; i< scorecard.Length - 1; i++)
        {   
            if (scorecard[i] == null) break;

            var number = (GameObject) Instantiate(number_pf, Vector3.zero, Quaternion.Euler((Vector3.zero)));  
            number.transform.parent = number_panel;
            number.transform.GetChild(0).GetComponent<TMP_Text>().text = "hole " + (i + 1);


            var score = (GameObject) Instantiate(score_pf, Vector3.zero, Quaternion.Euler((Vector3.zero)));
            score.transform.parent = score_panel;
            score.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>().text = "" + scorecard[i].par;
            score.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TMP_Text>().text = "" + scorecard[i].score;
        }
    }
}
