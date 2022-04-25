using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowBall : MonoBehaviour
{   

    public Camera camera;

    public Rigidbody ball_body;

    public Vector3 mousePressedPos, mouseReleasePos;
    public Vector3 currentVel;

    public float screenWidth, screenHeight;

    public float maxVel, minVel;

    public int clubLoft;

    private GameObject loftSlider;


    // Start is called before the first frame update
    void Awake()
    {
        loftSlider = GameObject.Find("Loft");
        screenWidth =  Screen.currentResolution.width; screenHeight =  Screen.currentResolution.height;

        
    }

    // Update is called once per frame
    void Update()
    {   
        if (ball_body.velocity != Vector3.zero)
            return;

        if (MouseIsOnClickableArea())
        {
            if (Input.GetMouseButtonDown(0))
            {   

                mousePressedPos = LimitMousePos(Input.mousePosition);
                //ball_body.velocity = camera.transform.forward * shootPower;
            }else if (Input.GetMouseButtonUp(0) && mousePressedPos != Vector3.zero)
            {   
                mouseReleasePos = LimitMousePos(Input.mousePosition);

                Shoot(mouseReleasePos - mousePressedPos);
            }
        }else
        {
            mousePressedPos = Vector3.zero;
            mouseReleasePos = Vector3.zero;
        }
        


        if (Input.GetMouseButton(0)) {
            currentVel = RelativeVel(LimitMousePos(Input.mousePosition) - mousePressedPos);
        }
        else 
            currentVel = Vector3.zero;

    }

    void FixedUpdate() {
        screenWidth =  Screen.width; screenHeight =  Screen.height;
    }

    private void Shoot(Vector3 Vel)
    {   


        Vector3 adjustedVel = RelativeVel(Vel);    

        if (!EnoughSpeed(adjustedVel))   
            return;

        GameSettings.instance.audioSource.PlayOneShot(GetComponent<PlayerController>().hit_sound);


        ball_body.velocity = adjustedVel;

        ball_body.angularVelocity = camera.transform.right * -clubLoft;
        AddStroke();
    }

    private Vector3 RelativeVel(Vector3 vel)
    {   

    
        Vector2 new_vel = new Vector2(
            vel.x / screenWidth * maxVel, 
            vel.y / screenHeight * maxVel
        ); 

        //loft modifier
        float y_loft = Mathf.Sin(clubLoft * Mathf.Deg2Rad);
        float z_loft = Mathf.Cos(clubLoft * Mathf.Deg2Rad);

        Vector3 camRelative = new Vector3(-camera.transform.forward.x, -1, -camera.transform.forward.z);
    
        camRelative.y *= y_loft;
        camRelative.z *= z_loft;
        camRelative.x *= z_loft;

        camRelative.x *= new_vel.y - new_vel.x;
        camRelative.y *= new_vel.y;
        camRelative.z *= new_vel.y;

        //camRelative.x *= z_loft;

        return camRelative;
    }

    private Vector3 LimitMousePos(Vector3 position)
    {
        return new Vector3(Mathf.Clamp(position.x, 0, screenWidth), Mathf.Clamp(position.y, 0, screenHeight), 0);
    }

    private void AddStroke()
    {   
        GetComponent<PlayerController>().AddStroke();
    }

    private bool EnoughSpeed(Vector3 vel){
        
        if (Mathf.Abs(vel.x) < minVel && Mathf.Abs(vel.y) < minVel && Mathf.Abs(vel.z) < minVel)
            return false;

        return true;
    }

    private bool MouseIsOnClickableArea()
    {   loftSlider = GameObject.Find("Loft");
        return !loftSlider.GetComponent<LoftController>().isTouching;
    }
    

    public void changeLoft(Slider slider)
    {
        clubLoft = (int) slider.value;
    }

}
