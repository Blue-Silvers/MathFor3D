using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMoove : MonoBehaviour
{
    private float speed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float horizontalSpeed;
    bool isRunning = false;
    private Vector2 input;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Camera cam;
    [SerializeField] int fovValue;
    int aiming = 0;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
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
        transform.rotation = transform.rotation * Quaternion.AngleAxis(- horizontalSpeed, new Vector3(input.x, input.y, 0));

        Vector3 movement = new Vector3(0, -(transform.forward.y * 1 * speed * Time.deltaTime), -(transform.forward.x * 1 * speed * Time.deltaTime));
        rigidbody.MovePosition(transform.position + movement);

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
}