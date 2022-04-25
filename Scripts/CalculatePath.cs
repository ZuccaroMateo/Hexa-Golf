using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatePath : MonoBehaviour
{

    public GameObject ball;
    public Rigidbody ball_rb;
    public Vector3 initialVel;

    public GameObject target;

    private float ballRadius = 0.5f / 2;

    public float timeStep = 0.1f;

    private ThrowBall throwBall_script;
    private DrawPath drawPath_script;

    private int array_size;


    

    // Start is called before the first frame update
    void Start()
    {
        throwBall_script = gameObject.transform.parent.gameObject.GetComponent<ThrowBall>();
        drawPath_script = gameObject.GetComponent<DrawPath>();

        array_size = drawPath_script.array_size;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //target.transform.position = CalculatePosInTime(initialForce, 1f);

        initialVel = throwBall_script.currentVel; 

        Visualize();

    }

    private Vector3 CalculatePosInTime(Vector3 vel, float time)
    {   
    
        Vector3 result = ball.transform.position + vel * time;

        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vel.y * time) + ball.transform.position.y;


        result.y = sY;

        return result;
    }

    private List<Vector3> CalculatePoints()
    {
        List<Vector3> list = new List<Vector3>();

        float timeElapsed = timeStep * 1.5f;

        Vector3 pos = CalculatePosInTime(initialVel, timeElapsed);
        
        int index = 0;
        while (!Physics.CheckSphere(pos, ballRadius) && index < array_size)
        {   
            list.Add(pos);

            timeElapsed += timeStep;
            pos = CalculatePosInTime(initialVel, timeElapsed);

            index += 1;
        }

        return list;
    }

    void Visualize()
    {   
        List<Vector3> list = CalculatePoints();

        if (list.Count == 0 || initialVel == Vector3.zero) 
        {   
            drawPath_script.deletePath();
            return;
        }
        

        drawPath_script.SetPath(list);

    }
}
