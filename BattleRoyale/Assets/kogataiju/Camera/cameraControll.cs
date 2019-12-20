using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControll : MonoBehaviour
{
    public GameObject player;

    //カメラとプレイヤーとの差分
    private Vector3 offset;

    public Transform verRot;
    public Transform horRot;

    private float zoom;
    private float view;

    private Camera cam;

    [SerializeField] private float Y_Rotation;
    // Start is called before the first frame update
    void Start()
    {
        verRot = transform.parent;
        horRot = GetComponent<Transform>();
        offset = transform.position - transform.parent.position;
        cam = GetComponent<Camera>();
        view = cam.fieldOfView;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        cam.fieldOfView = view + zoom;

        if(cam.fieldOfView > 10.0f)
        {
            cam.fieldOfView = 10.0f;
        }

        if(cam.fieldOfView < 60.0f)
        {
            cam.fieldOfView = 60.0f;
        }

        if (Input.GetMouseButtonDown(1))
        {
            zoom -= 0.3f;
        }
        if (Input.GetMouseButtonUp(1))
        {
            zoom += 0.3f;
        }
        

        float X_Rotation = Input.GetAxis("Mouse X");
        Y_Rotation = Input.GetAxis("Mouse Y");
        
        //transform.position = player.transform.position + offset; 
        transform.position = transform.parent.position + offset; 

        verRot.transform.Rotate(0, -X_Rotation, 0);
        horRot.transform.Rotate(-Y_Rotation, 0, 0);
    }
}
