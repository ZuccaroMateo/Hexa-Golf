using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;

    public Vector3 offset;

    public CinemachineFreeLook cinemachineFreeLook;

    public bool isMoving = false;

    void Update() {
    //    Vector3 desiredPosition = target.position + offset;
    //    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); 

    //    transform.position = smoothedPosition;

    //    transform.LookAt(target);

        if (Input.GetMouseButtonDown(1)){
            isMoving = true;
            cinemachineFreeLook.m_YAxis.m_InputAxisName = "Mouse Y";
            cinemachineFreeLook.m_XAxis.m_InputAxisName = "Mouse X";
        }
        else if (Input.GetMouseButtonUp(1)){
            isMoving = false;
            cinemachineFreeLook.m_YAxis.m_InputAxisName = "";
            cinemachineFreeLook.m_XAxis.m_InputAxisName = "";  
            cinemachineFreeLook.m_YAxis.m_InputAxisValue = 0;
            cinemachineFreeLook.m_XAxis.m_InputAxisValue = 0;  
        }

    }

}
