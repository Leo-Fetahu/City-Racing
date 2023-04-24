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
    public float breakingForce = 300f;
    public float maxTurnAngle = 15f;
    public float nitrousBoost = 1000f; // Amount of nitrous boost
    public float nitrousDuration = 5f; // Duration of nitrous boost

    private bool nitrousActivated = false; // Flag to track if nitrous is activated
    private float nitrousTimer = 0f; // Timer to track duration of nitrous boost

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    private void FixedUpdate() {

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
