using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 CameraPosition;

    [Header("Camera Settings")]
    public float CameraSpeed;
    public float zoomSize;


    void Start()
    {
        CameraPosition = this.transform.position;
        zoomSize = 15;
    }

    void Update()
    {
        //Camera Zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (zoomSize > 2)
                zoomSize -= 1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            zoomSize += 1;
        }
        GetComponent<Camera>().orthographicSize = zoomSize;

        //Camera Movement
        if (Input.GetKey(KeyCode.W))
        {
            CameraPosition.y += CameraSpeed / 50;
        }
        if (Input.GetKey(KeyCode.S))
        {
            CameraPosition.y -= CameraSpeed / 50;
        }
        if (Input.GetKey(KeyCode.D))
        {
            CameraPosition.x += CameraSpeed / 50;
        }
        if (Input.GetKey(KeyCode.A))
        {
            CameraPosition.x -= CameraSpeed / 50;
        }

        this.transform.position = CameraPosition;
    }
}