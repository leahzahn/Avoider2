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
        //cameraHalfHeight = Camera.main.pixelHeight * Camera.main.aspect;

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
