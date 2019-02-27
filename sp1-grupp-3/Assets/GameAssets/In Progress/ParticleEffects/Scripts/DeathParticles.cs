using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    ParticleSystem particles;

    void Start()
    {
        RespawnTrigger death = GetComponent<RespawnTrigger>();
        particles = GetComponent<ParticleSystem>();

        death.OnMove += PlayerDeath;
    }

    private void PlayerDeath(GameObject obj)
    {
        particles.Play();
    }
}
