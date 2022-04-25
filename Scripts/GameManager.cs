using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using HoleDataNamespace;


namespace HoleDataNamespace{
    public class HoleData {
        public int par;
        public int score;

        public HoleData(int newPar, int newScore){
            par = newPar;
            score = newScore;
        }
    } 
}


public class GameManager : MonoBehaviour
{   

    private GameObject player, canvas, course;

    private RoundData roundData;
    private GameSettings gameSettings;

    private PlayerController player_script;

    public TMP_Text strokes_text;

    private CourseCreator course_script;

    public static int hole_number = 0;

    public int holesAmount;

    public int parCount;

    private static bool firstInstantiation = false;

    //to record the scorecard
    public static HoleData[] scorecard = new HoleData[9];  

    public static int arrayIndex = 0;

    // Start is called before the first frame update
    void Awake()
    {   

        findGameObjects();

        player_script = player.GetComponent<PlayerController>(); 

        strokes_text = canvas.transform.GetChild(0).GetComponent<TMP_Text>();  

        course_script = course.GetComponent<CourseCreator>();
        
    }

    void Start() {

        holesAmount = RoundData.instance.holesPerRound;
        hole_number = RoundData.instance.currentHole;

        if (!firstInstantiation) {
            GameSettings.ChangeMusic();

            firstInstantiation = true;
        }

        setHoleCount();
        setScoreText();

        
        StartCoroutine(lateStart());



    }

    public void UpdateStrokes()
    {   
        strokes_text.text = player_script.strokes.ToString() + " strokes";
    }

    public void EndHole()
    {   

        RoundData.instance.addHole(Mathf.Min(player_script.strokes, course_script.par + 3), course_script.par);

        strokes_text.text = "0 strokes";
        
        if (hole_number < holesAmount) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        }else
        {   
            //DontDestroyOnLoad(this.gameObject);
            //Destroy(canvas);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

    }

    public void setParText()
    {   
        if (canvas != null)
            canvas.transform.GetChild(1).GetComponent<TMP_Text>().text = "PAR " + course_script.par;
    }

    public void setScoreText()
    {
        string s_score;

        int score = RoundData.instance.score;

        if (score == 0) 
            s_score = "E";
        else if (score < 0)
            s_score = score.ToString();
        else
            s_score = "+" + score.ToString();  

        canvas.transform.GetChild(4).GetComponent<TMP_Text>().text = s_score;
    }


    public void setHoleCount()
    {   
        canvas.transform.GetChild(3).GetComponent<TMP_Text>().text = "hole " + hole_number + " of " + holesAmount;
    }

    public void findGameObjects()
    {
        canvas = GameObject.Find("Canvas");
        player = GameObject.Find("Player");
        course = GameObject.Find("Course");
    }

    public IEnumerator lateStart()
    {   
        yield return new WaitForSeconds(0f);
        setParText();
        setScoreText();
    }

    public static void backToMenu()
    {   
        //gameObject.GetComponent<DontDestroyOnLoad>().enabled = false;
        RoundData.resetStats();
        Application.LoadLevel(0);
    }

    public void ResetDestroyOnLoad() {
        SceneManager.MoveGameObjectToScene(gameObject, gameObject.scene);
     }
}
