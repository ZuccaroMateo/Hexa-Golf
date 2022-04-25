using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Hole")
            transform.parent.gameObject.GetComponent<PlayerController>().Holed();
        else if (collision.gameObject.name == "Water")
            transform.parent.gameObject.GetComponent<PlayerController>().HazardDrop();
    }
}
