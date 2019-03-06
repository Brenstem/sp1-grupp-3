using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffectPlay : MonoBehaviour
{
    ParticleSystem particle;
    GroundCheck grounded;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        grounded = GetComponent<GroundCheck>();
    }

    void Update()
    {
        if (grounded.HasLanded() == true)
        {
            Debug.Log("meme");
            particle.Play();
        }
    }
}
