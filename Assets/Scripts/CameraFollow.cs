//Leah Zahn
//ID: 2341427
//zahn @chapman.edu
//CPSC236-03
//Avoider
//This is my own work, and I did not cheat on this assignment.

/*
 * CameraFollow.cs
 * This class tells the main camera to follow the player as they move around the map. 
 * The camera is clamped at the borders so that it will not go beyond the map even 
 * if the player is at the edge.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    private Vector3 smoothPos;
    private float smoothSpeed = 0.5f;

    public GameObject cameraLeftBorder;
    public GameObject cameraRightBorder;
    public GameObject cameraTopBorder;
    public GameObject cameraBottomBorder;

    private float cameraHalfWidth;
    private float cameraHalfHeight;

    // Start is called before the first frame update
    void Start()
    {
        cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        cameraHalfHeight = Camera.main.orthographicSize; 

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float borderLeft = cameraLeftBorder.transform.position.x + cameraHalfWidth;
        float borderRight = cameraRightBorder.transform.position.x - cameraHalfWidth;
        float borderTop = cameraTopBorder.transform.position.y - cameraHalfHeight;
        float borderBottom = cameraBottomBorder.transform.position.y + cameraHalfHeight;

        smoothPos = Vector3.Lerp(this.transform.position,
            new Vector3(Mathf.Clamp(followTransform.position.x, borderLeft, borderRight),
            Mathf.Clamp(followTransform.position.y, borderBottom, borderTop), this.transform.position.z), smoothSpeed);

        this.transform.position = smoothPos;
    }
}
