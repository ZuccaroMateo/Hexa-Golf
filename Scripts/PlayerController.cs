using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   

    public GameManager gameManager;
    public int strokes = 0;

    private int hole_par;
    private GameObject ball;

    private Rigidbody body;

    private float block_size = 1.5f;

    public Vector3 lastPosition;

    public AudioClip hit_sound, water_sound, holed_sound;


    // Start is called before the first frame update
    void Start()
    {
        ball = gameObject.transform.GetChild(0).gameObject;
        body = ball.GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        StartCoroutine(LateStart());
    }

    // Update is called once per frame
    void Update()
    {   
        if (
            (body.velocity == Vector3.zero && strokes >= hole_par + 2)  || 
            (strokes > hole_par + 3))
        {
            AddStroke();
            Holed();
        }
    }

    public void MoveBallTo(Vector3 pos)
    {   
        Vector3 new_pos = new Vector3(pos.x, pos.y + 2 * block_size, pos.z);
        ball.transform.position = new_pos;

        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
    }

    public void AddStroke(){
        lastPosition = ball.transform.position;
        strokes++;
        gameManager.UpdateStrokes();
        
    }

    public void Holed()
    {   
        GameSettings.instance.audioSource.PlayOneShot(holed_sound);
        gameManager.EndHole();
    }

    public void HazardDrop()
    {   
        GameSettings.instance.audioSource.PlayOneShot(water_sound);
        MoveBallTo(lastPosition);
        AddStroke();
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0f);
        hole_par = GameObject.Find("Course").GetComponent<CourseCreator>().par;
    }


}
