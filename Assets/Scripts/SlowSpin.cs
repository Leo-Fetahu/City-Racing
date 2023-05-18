using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSpin : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0f * Time.deltaTime, 0.02f, 0));
    }


}
