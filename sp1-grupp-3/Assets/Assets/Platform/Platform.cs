using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Platform : MonoBehaviour {
    public PathCreator pathCreator;
    public float speed = 5;
    public bool reversePath;
    float distanceTravelled;

	void FixedUpdate ()
    {
        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        distanceTravelled += speed * Time.deltaTime;

        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);

        Quaternion rotationOffsetY = Quaternion.Euler(Vector3.up * 90); //Makes 2D sprites face camera
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled) * rotationOffsetY;
    }    
}
