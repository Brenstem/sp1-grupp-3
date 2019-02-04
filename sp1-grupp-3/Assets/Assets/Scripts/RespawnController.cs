using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour {

    public Checkpoint respawnCheckPoint = null;

    public delegate void MyDelegate();
    public event MyDelegate onRespawn;

    Vector2 initialPos;

	void Awake () {
        initialPos = transform.position;
        respawnCheckPoint.onRespawn += OnRespawn;
	}
	
	public void OnRespawn()
    {
        transform.position = initialPos;
        onRespawn();
    }
}
