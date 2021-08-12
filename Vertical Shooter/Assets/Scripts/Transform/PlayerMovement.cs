using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f, bankingValue = 90f;

    public Transform visualChild;

    private Camera cam;

    private Rigidbody myRb;

    private float distance;

    private Vector3 velocity, lastPosition, rotation, touchPosition, screenToWorld;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        myRb = GetComponent<Rigidbody>();

        distance = (cam.transform.position - transform.position).y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = transform.position - lastPosition;

        Movement();

        lastPosition = transform.position;
    }

    void Movement() 
    {
        touchPosition = Input.mousePosition;

        touchPosition.z = distance;

        screenToWorld = cam.ScreenToWorldPoint(touchPosition);

        Vector3 movement = Vector3.Lerp(myRb.position, screenToWorld, speed * Time.fixedDeltaTime);

        myRb.MovePosition(movement);

        rotation.z = -velocity.x * bankingValue;

        myRb.MoveRotation(Quaternion.Euler(rotation));


    }
}
