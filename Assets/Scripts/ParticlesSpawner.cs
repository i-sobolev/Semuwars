using UnityEngine;

public class ParticlesSpawner : Singleton<ParticlesSpawner>
{
    [SerializeField] private ParticleSystem _sparksTemplate;
    [SerializeField] private ParticleSystem _bloodTemplate;

    private void SpawnParticles(Vector2 position, ParticleSystem particles)
    {
        var spawnedParticles = Instantiate(particles, position, Quaternion.identity);
        spawnedParticles.Play();
    }

    public void SpawnSparks(Vector2 position)
    {
        SpawnParticles(position, _sparksTemplate);
    }

    public void SpawnBlood(Vector2 position)
    {
        SpawnParticles(position, _bloodTemplate);
    }
}