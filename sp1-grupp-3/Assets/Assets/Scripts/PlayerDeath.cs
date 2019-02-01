using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    public delegate void MyDelegate();
    public event MyDelegate onDeath;

	void Start () {
		
	}
	
	void Update () {
		
	}

    void Death()
    {
        onDeath.Invoke();
    }
}
