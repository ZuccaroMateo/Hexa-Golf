using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{

    public int array_size = 20;
    public GameObject path_ball_pb;
    private GameObject[] balls;

    private Vector3 zero_pos = new Vector3(0, -10, 0);

    // Start is called before the first frame update
    void Start()
    {   

        //init balls under the world, so you don't see them
        balls = new GameObject[array_size];

        for (int i = 0; i < array_size; i++)
        {
            Vector3 random_rot = new Vector3(Random.Range(1,360), Random.Range(1,360), Random.Range(1,360));
            balls[i] = (GameObject) Instantiate(path_ball_pb, zero_pos, Quaternion.Euler(random_rot)) as GameObject;    
            balls[i].transform.parent = gameObject.transform;
        }
    }

    public void SetPath(List<Vector3> positions)
    {   
        int index = 0;
        foreach (Vector3 pos in positions)
        {
            balls[index].transform.position = pos;
            index += 1; 
        }

        if (index == array_size - 1) return;
        else
        {
            for (int i = index; i < array_size; i++)
                balls[i].transform.position = zero_pos;
        }
    }

    public void deletePath()
    {
        for (int i = 0; i < array_size; i++)
            balls[i].transform.position = zero_pos;
    }
}
