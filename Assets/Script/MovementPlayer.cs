using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementPlayer : MonoBehaviour
{
    private float speed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float horizontalSpeed;

    bool isRunning = false;
    private Vector2 input;
    [SerializeField] Rigidbody rigidbody;

    private bool jump = false, isGrounded = true;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float _jumpStrength = 8f;
    [SerializeField] Camera cam;
    [SerializeField] int fovValue;
    int aiming = 0;

    [SerializeField] bool backStab;


    Vector3 eF;
    float dotConversion, dotDirection;
    [SerializeField] float colisionDistance, backStabAngle;

    [SerializeField] GameObject spotted, canBackstab;

    private void Start()
    {

        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {


        GameObject[] enemy;
        enemy = GameObject.FindGameObjectsWithTag("enemy");

        foreach (GameObject cutHit in enemy)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                eF = transform.position - cutHit.transform.position;
                dotConversion = Vector3.Dot(cutHit.transform.forward, eF.normalized);
                dotDirection = Vector3.Dot(cutHit.transform.forward, transform.forward);

                    if (dotConversion < 0.8 && dotDirection > backStabAngle && eF.magnitude <= colisionDistance)
                    {
                        if (backStab)
                        {
                            print("GG");
                            cutHit.GetComponent<MeshRenderer>().material.color = Color.red; 
                            Destroy(cutHit, 0.5f);
                        }
                        else
                        {
                            print("Retry");
                        }
                    }
            }

            eF = transform.position - cutHit.transform.position;
            dotConversion = Vector3.Dot(cutHit.transform.forward, eF.normalized);
            dotDirection = Vector3.Dot(cutHit.transform.forward, transform.forward);

            if (dotConversion < 0.8 && dotDirection > backStabAngle && eF.magnitude <= colisionDistance && spotted.activeSelf == false)
            {
                canBackstab.SetActive(true);
                backStab = true;
            }
            else
            {
                canBackstab.SetActive(false);
                backStab = false;
            }

            GameObject[] suspect;
            suspect = GameObject.FindGameObjectsWithTag("Sus");
            if (suspect.Length > 0)
            {
                spotted.SetActive(true);
            }
            else 
            { 
                spotted.SetActive(false); 
            }
        }

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


        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            isGrounded = false;
        }
    }
    void FixedUpdate()
    {
        if (jump)
        {
            rigidbody.AddForce(Vector2.up * _jumpStrength, ForceMode.Impulse);
            jump = false;
        }

        Vector3 desireRotation = new Vector3(0, input.x * horizontalSpeed * Time.deltaTime, 0);
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(desireRotation));


        Vector3 movement = new Vector3(transform.forward.x * input.y * speed * Time.deltaTime, 0, transform.forward.z * input.y * speed * Time.deltaTime);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }

}
