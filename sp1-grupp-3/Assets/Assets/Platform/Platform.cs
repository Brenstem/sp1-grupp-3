using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Platform : MonoBehaviour {
    public PathCreator pathCreator;
    public float speed = 5;
    public bool reversePath = false;
    public bool turnAlongPath = false;
    public float totalDistance;
    public float distancePercentTotal;
    public float distancePercent;

    private float distanceTravelled;
    private bool move = true;

    private void Start()
    {
        
    }

    void FixedUpdate ()
    {        
        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        if (move) {
            distanceTravelled += speed * Time.deltaTime;

            //Wait();

            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Reverse);

            //Quaternion rotationOffsetY = Quaternion.Euler(Vector3.up * 90); //Makes 2D sprites face camera
            //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled) * rotationOffsetY;
        }
    }
    
    private void Wait()
    {
        totalDistance = pathCreator.path.length;
        distancePercentTotal = (distanceTravelled / totalDistance);
        distancePercent = distancePercentTotal % 1;

        if (distancePercentTotal >= 1) {
            move = false;
        }
    }
}
