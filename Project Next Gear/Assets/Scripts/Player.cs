using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private float forwardSpeed = 0;
    public float acceleration;
    private float lateralSpeed = 0;
    public float turnSpeed;
    public float tiltSpeed;
    public float tiltLimit;

    public float deadZone = 0;

    private Rigidbody rb;
    private Transform leftTilt;
    private Transform rightTilt;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        leftTilt = GameObject.Find("LeftTiltAxis").transform;
        rightTilt = GameObject.Find("RightTiltAxis").transform;

    }
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(transform.localRotation.z) < (Mathf.Deg2Rad * tiltLimit) /2)
        {
            forwardSpeed += acceleration * Time.deltaTime;
            if (Input.GetAxis("Horizontal") > deadZone)
            {
                lateralSpeed = turnSpeed * Input.GetAxis("Horizontal");
                transform.RotateAround(rightTilt.position, transform.forward, -tiltSpeed * Time.deltaTime);
            }
            else if (Input.GetAxis("Horizontal") < -deadZone)
            {
                lateralSpeed = turnSpeed * Input.GetAxis("Horizontal");
                transform.RotateAround(leftTilt.position, transform.forward, tiltSpeed * Time.deltaTime);
            }
            else
            {
                lateralSpeed = 0;
                if (transform.rotation.z > Quaternion.Euler(0, 0, 0.5f).z)
                {
                    transform.RotateAround(rightTilt.position, transform.forward, -(tiltSpeed * 2) * Time.deltaTime);
                }
                else if (transform.rotation.z < Quaternion.Euler(0, 0, -0.5f).z)
                {
                    transform.RotateAround(leftTilt.position, transform.forward, (tiltSpeed * 2) * Time.deltaTime);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            if (transform.rotation.z > 0)
            {
                transform.position = new Vector3(transform.position.x , 0 + (transform.position.y - leftTilt.position.y), transform.position.z);
            }
            else if (transform.rotation.z < 0)
            {
                transform.position = new Vector3(transform.position.x, 0 + (transform.position.y - rightTilt.position.y), transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            }
            

            rb.velocity = new Vector3(lateralSpeed, rb.velocity.y, forwardSpeed);




        }
        else
        {
            rb.useGravity = true;
        }

    }

}
