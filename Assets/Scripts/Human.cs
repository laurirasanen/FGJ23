using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public ParticleSystem BloodParticles;

    public void Explode()
    {
        var particles = Instantiate(BloodParticles, transform.position, Quaternion.identity);
        Destroy(particles.gameObject, 10.0f);
        Destroy(gameObject);
    }
}
