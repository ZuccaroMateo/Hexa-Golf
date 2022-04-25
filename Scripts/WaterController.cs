using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{   

    public GameObject water_pf;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = -10; x < 11; x++)
        {
            for (int z = -10; z < 11; z++)
            {
                spawnWater(new Vector3(x * 5f, 0, z * 5f));
            }  
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnWater(Vector3 pos) 
    {
        var water = (GameObject) Instantiate(water_pf, pos, gameObject.transform.rotation);  
        water.transform.parent = gameObject.transform;
    }
}
