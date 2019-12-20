using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private CharacterController charaCon;
    private Vector3 moveDir;
    private int gravity = 6;
    [SerializeField] private float speed = 0;
    [SerializeField] GameObject camera;

    float speedy = 0;

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
            if (Input.GetKey("s"))
            {
                speedz = -1;
            }
            if (Input.GetKey("d"))
            {
                speedx = 1;
            }
            if (Input.GetKey("a"))
            {
                speedx = -1;
            }

            if (Input.GetKey("left shift"))
            {
                speed = 10;
            }
            else
            {
                speed = 6;
            }

            var _inputX = Input.GetAxis("Mouse X");
            var _inputY = Input.GetAxis("Mouse Y");

            float maxLimit = 60, minLimit = 360 - maxLimit;

            //X軸回転
            var localAngle = transform.localEulerAngles;

            localAngle.x -= _inputY;

            if (localAngle.x > maxLimit && localAngle.x < 180)
                localAngle.x = maxLimit;

            if (localAngle.x < minLimit && localAngle.x > 180)
                localAngle.x = minLimit;

            transform.localEulerAngles = localAngle;

            //Y軸回転
            var angle = transform.eulerAngles;
            angle.y -= _inputX;
            transform.eulerAngles = angle;

            moveDir = new Vector3(speedx, speedy, speedz);

            moveDir = transform.TransformDirection(moveDir);

            moveDir *= speed;

            if (Input.GetKey("space"))
            {
                moveDir.y = 3;
            }
        }

        //重力
        moveDir.y -= gravity * Time.deltaTime;

        //毎フレーム動かす
        charaCon.Move(moveDir * Time.deltaTime);
    }
}
