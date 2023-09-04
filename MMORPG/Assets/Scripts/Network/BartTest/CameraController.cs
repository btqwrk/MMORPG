using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraController : MonoBehaviour
{
    public float rotationSensitivity;
    public float zoomSensitivity;

    public CinemachineFreeLook CinemachineCam;


    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            CinemachineCam.m_XAxis.Value += mouseX;
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float scrollY = Input.GetAxis("Mouse ScrollWheel");
            CinemachineCam.m_YAxis.Value += scrollY;
        }
    }



}

