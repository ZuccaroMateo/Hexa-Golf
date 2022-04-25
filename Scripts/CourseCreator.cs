using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creates a random map
public class CourseCreator : MonoBehaviour
{   
    
    public bool isMenu;
    public int par;

    private int hole_height, course_width, course_depth, tee_x;

    private string[,] hole_path; //2d array (x, z) of the hole path


    //blocks
    private GameObject grass_dk_pf, dirt_pf, dirt_dk_pf, tree_pf, hole_pf, grass_pf;

    private Material skybox, fog_color, light_color;

    //player
    private GameObject player;
    private PlayerController player_script;

    private float block_size = 2f;

    private string biomaType;


    // Start is called before the first frame update
    void Start()
    {   

        par = Random.Range(2,6);

        int random = Random.Range(0,3);

        if (random == 1)
            biomaType = "forest";
        else if (random == 2)
            biomaType = "meadow"; 
        else
            biomaType = "beach"; 

        ImportPrefabs(); 

        RenderSettings.skybox = skybox;
        RenderSettings.fogColor = fog_color.color;
        GameObject.Find("DirectionalLight").GetComponent<Light>().color = light_color.color;
    

        //init map with params
        hole_height = 10;
        course_width = 30;
        course_depth = par * 8;

        createMap();

        if (!isMenu) {
            player = GameObject.Find("Player");
            player_script = player.GetComponent<PlayerController>();
            spawnPlayer();
            //GameObject.Find("GameManager").GetComponent<GameManager>().setParText();
        }
        
    }


    private void createMap(){

        hole_path = getHolePath(course_width, course_depth);

        
        float y = 1;

        for (int x = 0; x < course_width; x++)
        {   
            for (int z = 0; z < course_depth; z++)
            {   
                float current_height = (float) Random.Range(hole_height - 2,hole_height); 


                if (hole_path[x,z] == "hole") //dont put anything on hole's position
                    continue;
                else if (hole_path[x,z] == "grass") //check if it is a path column   
                    spawnBlock(grass_pf, new Vector3(x,y,z), hole_height);
                
                else if (
                    ((x + 1 < course_width && hole_path[x + 1,z] == "grass")
                        || 
                    (x > 1 && hole_path[x - 1, z] == "grass")) &&

                    y < hole_height - 1 &&

                    hole_path[x,z] == "dirt"
                    ) //check if it is a side-path column but not reaching the path height
                {   
                    spawnBlock(grass_dk_pf, new Vector3(x,y,z), hole_height - 1);
                }

                else if(y <= current_height && hole_path[x,z] == "dirt") //if it's not a path or a side path, do the current height max   
                {   
                    spawnRandomDirtBlock(new Vector3(x,y,z), current_height);

                
                    int random = 0;

                    if (biomaType == "beach")
                        random = Random.Range(0,20);
                    else if (biomaType == "forest")
                        random = Random.Range(0,8);
                    else if (biomaType == "meadow")
                        random = Random.Range(0,30);


                    if (current_height == hole_height - 2 && random == 5) //we may spawn a tree
                    {
                        spawnTree(
                            new Vector3( x * block_size,
                            (float) (current_height * block_size) + tree_pf.transform.localScale.y / 2,
                            z * block_size)
                        );
                    }
                }
                
            }
        }
            
        createBorderBlocks();
        spawnHole();
        
    }

    //gets a path for the hole in the surface of the course
    //what we do is iterate over the depth making a 2d path with turns and straights
    private string[,] getHolePath(int width, int depth){
        string[,] path = new string[width, depth];

        int last_x = (int) width/2 + 1;
        int new_x;

        for (int z = 0; z < depth; z++)
        {   
            int x_desviation = Random.Range(-3,4); //the maximum desviation in the x-axis is 3
            new_x = Mathf.Clamp(last_x + x_desviation, 0, width - 1); //with ensure that the number is in the course range
            
            for (int x = 0; x < width; x++)
            {   
                
                bool hole = itsTheHole(x,z);

                if (hole) 
                    path[x,z] = "hole"; 
                
                else if ( x == new_x || (x >= last_x && x <= new_x) || (x >= new_x && x <= last_x)) 
                    path[x,z] = "grass";
                else
                    path[x,z] = "dirt";
                

                //save tee's position
                if (z == depth - 1 && x == new_x)
                    tee_x = x;
            }
            last_x = new_x;
        }

        return path;
    }

    //so it looks like an island
    private void createBorderBlocks()
    {   
        //back side
        for (int x = 0; x < course_width; x++)
        {   
            int height = hole_height - 4;

            for (int z = -1; z > -6; z--) 
            {   
                height -= 1;

                int random = Random.Range(0,2);

                if (z < -4 && random == 1)
                {
                    continue;
                }

                random = Random.Range(-1,2);

                spawnRandomDirtBlock(new Vector3(x,1,z), height + random);   
            }
        }


        //front side
        for (int x = 0; x < course_width; x++)
        {   
            int height = hole_height - 4;

            for (int z = course_depth; z < course_depth + 7; z++) 
            {   
                height -= 1;

                int random = Random.Range(0,2);

                if (z > course_depth + 5 && random == 1)
                {
                    continue;
                }

                random = Random.Range(-1,2);

                spawnRandomDirtBlock(new Vector3(x,1,z), height + random);   
            }
        }


        //rigth side
        for (int z = -5; z < course_depth + 6; z++) 
        {
            int height = hole_height - 4;

            for (int x = course_width; x < course_width + 6; x++)
            {
                height -= 1;

                int random = Random.Range(0,2);

                if (x > course_width + 3 && random == 1)
                {
                    continue;
                }

                random = Random.Range(-1,2);

                spawnRandomDirtBlock(new Vector3(x,1,z), height + random);   
            }
        } 

        //left side
        for (int z = -5; z < course_depth + 6; z++) 
        {
            int height = hole_height - 4;

            for (int x = -1; x > -6; x--)
            {
                height -= 1;

                int random = Random.Range(0,2);

                if (x > course_width + 3 && random == 1)
                {
                    continue;
                }

                random = Random.Range(-1,2);

                spawnRandomDirtBlock(new Vector3(x,1,z), height + random);   
            }
        } 

    }



    private void spawnBlock(GameObject prefab, Vector3 pos, float height) 
    {
        var cube = (GameObject) Instantiate(prefab, pos * block_size, gameObject.transform.rotation);  
        cube.transform.parent = gameObject.transform;
        cube.transform.localScale += new Vector3(cube.transform.up.x * 0, cube.transform.up.y * cube.transform.localScale.y * height * block_size, cube.transform.up.z * cube.transform.localScale.z * height);
        cube.transform.position = new Vector3(cube.transform.position.x, cube.transform.position.y + (float) height / 2 * block_size, cube.transform.position.z);    
    }

    private void spawnRandomDirtBlock(Vector3 pos, float height)
    {
        int random = Random.Range(0,10);

        if (random == 1) 
        {
            spawnBlock(dirt_dk_pf, pos, height);
        }else 
        {
            spawnBlock(dirt_pf, pos, height);
        }
    }

    //we iterate thorugh the course and place trees in random places of the hightest blocks off-road
    private void spawnTree(Vector3 pos)
    {   
        var tree = (GameObject) Instantiate(tree_pf, pos, gameObject.transform.rotation);  
        tree.transform.parent = gameObject.transform;
        int random = Random.Range(0,360);

        tree.transform.Rotate(0, random, 0);
    }

    private void spawnHole()
    {   
        Vector3 pos = new Vector3((int) (course_width / 2) + 1, hole_height + 2, 2) * block_size;
        var hole = (GameObject) Instantiate(hole_pf, pos, gameObject.transform.rotation);  
        hole.transform.parent = gameObject.transform;
    }

    private void spawnPlayer()
    {
        player_script.MoveBallTo(new Vector3(tee_x, hole_height + 2, course_depth - 1) * block_size);
    }

    private bool itsTheHole(int x, int z)
    {   
        bool yes = (x == ((int) course_width / 2) + 1 && z == 2);
        return yes;   
    }


    private void ImportPrefabs()
    {
        grass_dk_pf = Resources.Load("Prefabs/" + biomaType +"/grass_dk_pf") as GameObject;
        grass_pf = Resources.Load("Prefabs/" + biomaType +"/grass_pf") as GameObject;
        dirt_dk_pf = Resources.Load("Prefabs/" + biomaType +"/dirt_dk_pf") as GameObject;
        dirt_pf = Resources.Load("Prefabs/" + biomaType +"/dirt_pf") as GameObject;
        hole_pf = Resources.Load("Prefabs/" + biomaType +"/hole_pf") as GameObject;
        tree_pf = Resources.Load("Prefabs/" + biomaType +"/tree_pf") as GameObject;
        skybox = Resources.Load("Materials/" + biomaType + "_skybox") as Material;
        fog_color = Resources.Load("Materials/" + biomaType + "_color") as Material;
        light_color = Resources.Load("Materials/" + biomaType + "_light") as Material;
    }
}