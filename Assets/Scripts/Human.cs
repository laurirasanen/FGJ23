using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public ParticleSystem BloodParticles;
    public Animator Anim;


    public void Explode()
    {
        Anim.SetTrigger("Die");
        var particles = Instantiate(BloodParticles, transform.position, Quaternion.identity);
        Destroy(particles.gameObject, 10.0f);
    }
    public void Despawn()
    {
        Destroy(gameObject);
    }
}
