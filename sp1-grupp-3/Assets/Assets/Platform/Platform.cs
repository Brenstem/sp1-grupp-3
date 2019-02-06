using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Platform : MonoBehaviour {
    public PathCreator pathCreator;
    public float speed = 5;
    public bool reversePath = false;
    public bool turnAlongPath = false;
    public Vector2 velocity;

    [SerializeField] private bool move = true;
    private float distanceTravelled;
    private Vector2 lastPosition;
            
    
    // Debug
    public float totalDistance;
    public float distancePercentTotal;
    public float distancePercent;

    public GameObject testBlock;

    private void Start()
    {
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        Instantiate(testBlock, transform.position + (Vector3.up * 3), Quaternion.identity);
    }

    private void Update()
    {
        velocity = GetKinematicVelocity();
    }

    void FixedUpdate ()
    {        
        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        if (move) {
            distanceTravelled += speed * Time.deltaTime;
        }

        //Wait();

        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Reverse);

        //Quaternion rotationOffsetY = Quaternion.Euler(Vector3.up * 90); //Makes 2D sprites face camera
        //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled) * rotationOffsetY;
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

    private Vector2 GetKinematicVelocity()
    {
        Vector2 currentPosition = transform.position;
        Vector2 velocityReturn = currentPosition - lastPosition;
        lastPosition = currentPosition;

        return velocityReturn;
    }

}
