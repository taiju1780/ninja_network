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

    [SerializeField] private float Y_Rotation;
    // Start is called before the first frame update
    void Start()
    {
        verRot = transform.parent;
        horRot = GetComponent<Transform>();
        offset = transform.position - transform.parent.position;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        float X_Rotation = Input.GetAxis("Mouse X");
        Y_Rotation = Input.GetAxis("Mouse Y");

        Matrix4x4 mat;
        mat = Matrix4x4.identity;
        mat.SetTRS(transform.position, transform.rotation, new Vector3(1, 1, 1));


        offset.y = offset.y + Y_Rotation / 100;
        //transform.position = player.transform.position + offset; 
        transform.position = transform.parent.position + offset; 

        verRot.transform.Rotate(0, -X_Rotation, 0);
        horRot.transform.Rotate(-Y_Rotation, 0, 0);
    }
}
