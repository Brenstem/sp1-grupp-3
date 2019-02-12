using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField]
    float deathTimer;

    void Start()
    {
        Destroy(gameObject, deathTimer);
    }
}
