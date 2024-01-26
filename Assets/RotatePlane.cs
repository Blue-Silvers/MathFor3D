using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlane : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    private Vector2 input;

    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    }
    void FixedUpdate()
    {
        transform.rotation = transform.rotation * Quaternion.AngleAxis(- horizontalSpeed, new Vector3(0 , input.x,0));
    }
}
