using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Rigidbody target;

    public float maxSpeed = 0.0f; // The maximum speed of the car ** in KM/H **

    public float minSpeedNeedleAngle;
    public float maxSpeedNeedleAngle;

    [Header("UI")]
    public TextMeshProUGUI speedLabel; // The label that displays the speed
    public RectTransform needle; // The needle in the speedometer

    private float speed = 0.0f;

    private void Update()
    {
        // 3.6f to convert to KM/H
        speed = target.velocity.magnitude * 3.6f;

        if (speedLabel != null)
            speedLabel.text = ((int)speed) + " km/h";
        if (needle != null)
            needle.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedNeedleAngle, maxSpeedNeedleAngle, speed / maxSpeed));
    }
}
