using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour {
    
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform backRightTransform;
    [SerializeField] Transform backLeftTransform;

    public float acceleration = 500f;
    public float breakingForce = 250f;
    public float maxTurnAngle = 15f;
    public float nitrousBoost = 2000f; // Amount of nitrous boost
    public float nitrousDuration = 7f; // Duration of nitrous boost

    private bool nitrousActivated = false; // Flag to track if nitrous is activated
    private float nitrousTimer = 0f; // Timer to track duration of nitrous boost

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    private float currentSpeed = 0f; //Current Speed of the car
    public float topSpeed = 100f; //Maximum speed of car
    

    private void FixedUpdate() {

        // Calculate the current speed of the car based on the average speed of all four wheels
        currentSpeed = (frontLeft.rpm + frontRight.rpm + backLeft.rpm + backRight.rpm) / 4f * 0.10472f * frontLeft.radius; // Convert rpm to m/s


        if (currentSpeed > topSpeed)
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * (topSpeed / 2.237f);

        

        // Get forward/reverse acceleration from the vertical axis (W and S keys)
        currentAcceleration = acceleration * Input.GetAxis("Vertical");


        // If we're pressing space, give currentBreakingForce a value.
        if (Input.GetKey(KeyCode.Space))
            currentBreakForce = breakingForce;
        else
            currentBreakForce = 0f;

        // Nitrous boost activation
        if (Input.GetKeyDown(KeyCode.N) && !nitrousActivated) {
            nitrousActivated = true;
            nitrousTimer = 0f;
        }

        // Nitrous boost duration
        if (nitrousActivated && nitrousTimer < nitrousDuration) {
            currentAcceleration += nitrousBoost;
            nitrousTimer += Time.deltaTime;
        } else {
            nitrousActivated = false;
        }

        // Apply acceleration to front wheels
        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;

        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;


        // Take care of the steering.
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        // Update wheel meshes.
        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(backLeft, backLeftTransform);
        UpdateWheel(backRight, backRightTransform);

    }

    void UpdateWheel(WheelCollider col, Transform trans) {

        // Get wheel collider state.
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        // Set wheel transform state.
        trans.position = position;
        trans.rotation = rotation;

    }

}
