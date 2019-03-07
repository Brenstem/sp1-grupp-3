using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    ParticleSystem particles;

    void Start()
    {
        Death death = GetComponentInParent<Death>();
        particles = GetComponent<ParticleSystem>();

        death.OnPlayerDeath += PlayerDeath;
    }

    private void PlayerDeath()
    {
        particles.Play();
    }
}
