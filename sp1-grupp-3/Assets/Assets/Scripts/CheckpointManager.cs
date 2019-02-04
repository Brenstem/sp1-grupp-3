using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public List<Checkpoint> CheckPoints {
        get { return checkPoints; }
    }

    public Checkpoint currentCheckPoint => checkPoints == null ? checkPoints[currentIndex] : null;
    public static CheckpointManager Instance => instance;

    List<Checkpoint> checkPoints = new List<Checkpoint>();
    int currentIndex = 0;
    static CheckpointManager instance = null;

    protected void Awake()
    {
        instance = this;

        //Hitta alla Checkpoint-Childs i gameobject (CheckpointManager)
        for (int i = 0; i < transform.childCount; i++) {
            Checkpoint checkpoint = transform.GetChild(i).GetComponent<Checkpoint>();
            checkpoint.OnTrigger += CheckpointTrigger;
            checkPoints.Add(checkpoint);
        }
    }

    public void CheckpointTrigger(Checkpoint newCheckpoint)
    {
        currentIndex = checkPoints.IndexOf(newCheckpoint);
    }
}

