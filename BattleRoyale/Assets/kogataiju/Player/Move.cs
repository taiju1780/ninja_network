using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController charaCon;
    private Vector3 moveDir;
    private int gravity = 6;
    [SerializeField] private float speed = 0;

    float speedy = 0;

    public GameObject cam;
    public GameObject chara;
   
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        charaCon = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        float speedx = 0f, speedz = 0f;

        if (charaCon.isGrounded)
        {
            if (Input.GetKey("w"))
            {
                speedz = 1;
            }
            else if (Input.GetKey("left shift") && Input.GetKey("w"))
            {
                speedz = 2;
            }
            if (Input.GetKey("s"))
            {
                speedz = -1;
            }
            else if (Input.GetKey("left shift") && Input.GetKey("s"))
            {
                speedz = -2;
            }
            if (Input.GetKey("d"))
            {
                speedx = 1;
            }
            else if (Input.GetKey("left shift") && Input.GetKey("d"))
            {
                speedx = 2;
            }
            if (Input.GetKey("a"))
            {
                speedx = -1;
            }
            else if (Input.GetKey("left shift") && Input.GetKey("d"))
            {
                speedx = -2;
            }

            if(Input.GetKey("left shift"))
            {
                speed = 10;
            }
            else
            {
                speed = 6;
            }

            moveDir = new Vector3(speedx, speedy, speedz);

            moveDir = transform.TransformDirection(moveDir);

            moveDir *= speed;

            if (Input.GetKey("space"))
            {
                moveDir.y = 3;
            }
           
        }

        chara.transform.rotation = cam.transform.rotation;

        //重力
        moveDir.y -= gravity * Time.deltaTime;

        //毎フレーム動かす
        charaCon.Move(moveDir * Time.deltaTime);
    }
}
