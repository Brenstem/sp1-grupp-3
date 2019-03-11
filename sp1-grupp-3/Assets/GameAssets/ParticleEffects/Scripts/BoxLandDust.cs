using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLandDust : MonoBehaviour
{
    ParticleSystem particle;
    SoundEvent sound;
    GroundCheck gc;
    bool previousGrounded = false;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        gc = GetComponentInParent<GroundCheck>();
        sound = GetComponent<SoundEvent>();
    }

    void Update()
    {
        if (gc.isGrounded) {
            if (previousGrounded == false) {
                particle.Play();
                sound.PlayOneShot("event:/Object/Box/box_1stland");
                previousGrounded = gc.isGrounded;
            }
        }
        previousGrounded = gc.isGrounded;
    }
}
