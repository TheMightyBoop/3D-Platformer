using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float ySpeed = 1f;
    public float xSpeed = 1f;

    public bool yAxis = false;
    public bool xAxis = false;

    // Update is called once per frame
    void Update () {
        if (yAxis)
        {
            transform.Rotate(Vector3.up * ySpeed);
        }

        if (xAxis)
        {
            transform.Rotate(Vector3.right * xSpeed);
        }
    }
}
