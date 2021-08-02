using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathController : MonoBehaviour
{
    
    public PathCreator pathCreator;
    public float speed;
    float distanceTravelled;

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = pathCreator.path.GetPointAtDistance(-distanceTravelled);
        }
    }
}
