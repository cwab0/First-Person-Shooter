using System.Collections;
using UnityEngine;

public class DeleteParticle : MonoBehaviour
{
    ParticleSystem particle;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();

    }
    private void Update()
    {
        if (!particle.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}