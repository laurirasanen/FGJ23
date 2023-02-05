using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Human : MonoBehaviour
{
    public ParticleSystem BloodParticles;
    public Animator Anim;
    public List<AudioClip> DeathSounds;
    public bool IsDead;

    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Explode()
    {
        if (IsDead)
        {
            return;
        }
        IsDead = true;
        
        Anim.SetTrigger("Die");

        var particles = Instantiate(BloodParticles, transform.position, Quaternion.identity);
        Destroy(particles.gameObject, 10.0f);

        var colliders = GetComponents<Collider>();
        foreach (var c in colliders)
        {
            c.enabled = false;
        }

        var sound = DeathSounds[Random.Range(0, DeathSounds.Count)];
        audioSource.clip = sound;
        audioSource.enabled = true;
        audioSource.Play();
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
