using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidGenerator : MonoBehaviour
{

    public List<FluidParticle> particles = new List<FluidParticle>();

    public FluidParticle particlePrefab;

    [Range(0.0f, 0.5f)]
    public float randAmount = 0.0f;

    public void SpawnParticles(List<Material> materials)
    {
        foreach (Material mat in materials)
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-randAmount, randAmount), Random.Range(-randAmount, randAmount));
            FluidParticle particle = Instantiate<FluidParticle>(particlePrefab, pos, Quaternion.identity, transform);
            particle.worldPosition = pos;
            particle.spawnPosition = pos;
            particle.GetComponent<SpriteRenderer>().material = mat;
            particles.Add(particle);
        }
    }

    public void UpdateParticle(int which)
    {
        particles[which].Move();
    }

    public void ResetParticle(int which)
    {
        particles[which].Reset();
    }

}
