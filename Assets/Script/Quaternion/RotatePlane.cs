using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlane : MonoBehaviour
{
    private float speed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float horizontalSpeed;
    bool isRunning = false;
    private Vector2 input;
    [SerializeField] private Vector3 magicAngle;
    [SerializeField] GameObject plane;
    private Rigidbody rb;
    [SerializeField] Camera cam;
    [SerializeField] int fovValue;
    [SerializeField] Quaternion Q1, Q2;
    int aiming = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Mouse1))
        {
            aiming = 1;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {

            aiming = 2;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            isRunning = true;
        }
        else
        {
            speed = walkSpeed;
            isRunning = false;
        }

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    }
    void FixedUpdate()
    {

        float angle = horizontalSpeed * Time.deltaTime * - input.x *2;
        plane.transform.localRotation *= Quaternion.AngleAxis(angle, magicAngle);
        if (plane.transform.localRotation == Q1)
        {
            //print("Right");
            transform.rotation *= GetQuaternionFromAngleRightLeft((horizontalSpeed * 1 * Time.deltaTime) * Mathf.Deg2Rad, transform.up);
        }
        if (plane.transform.localRotation == Q2)
        {
            //print("Left");
            transform.rotation *= GetQuaternionFromAngleRightLeft((horizontalSpeed * -1 * Time.deltaTime) * Mathf.Deg2Rad, transform.up);
        }

        transform.rotation *= GetQuaternionFromAngleUp((horizontalSpeed * input.y * Time.deltaTime) * Mathf.Deg2Rad, transform.right);


        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (aiming == 1)
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue - fovValue * 40 / 100, 2f);
        }
        else if (aiming == 2)
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue, 2f);
            if (cam.fieldOfView == fovValue)
            {
                aiming = 0;
            }
        }
        else
        {
            if (input.sqrMagnitude != 0)
            {
                if (isRunning == true)
                {
                    cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue + fovValue * 20 / 100, 0.5f);
                }
                else
                {
                    cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue + fovValue * 10 / 100, 0.5f);
                }
            }
            else
            {
                cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue, 0.5f);
            }
        }

    }

    Quaternion GetQuaternionFromAngleRightLeft(float angle, Vector3 axis)
    {
        return new Quaternion(
                0,
                (Mathf.Sin(angle) / 2) * axis.y,  
                0,  //z
                (Mathf.Cos(angle) / 2)        
            );
    }

    Quaternion GetQuaternionFromAngleUp(float angle, Vector3 axis)
    {
        return new Quaternion(
                (Mathf.Sin(angle) / 2) * axis.x, 
                0,  
                0,  
                (Mathf.Cos(angle) / 2)          
            );
    }

}
