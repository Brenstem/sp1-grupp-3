using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkDustParticles : MonoBehaviour
{
    GroundCheck gc;
    ParticleSystem particle;

    private void Start()
    {
        
        gc = GetComponentInParent<GroundCheck>();
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!gc.isGrounded) // ?????
        {
            particle.Play();
        }
    }


}
