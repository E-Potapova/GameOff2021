using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    void Start()
    {
        ParticleSystem parts = gameObject.GetComponent<ParticleSystem>();
        parts.Play();
        float totalDuration = parts.main.duration + parts.main.startLifetime.constant;
        Destroy(gameObject, totalDuration);
    }
}
