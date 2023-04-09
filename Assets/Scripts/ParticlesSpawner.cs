using Unity.Netcode;
using UnityEngine;

public class ParticlesSpawner : NetworkSingleton<ParticlesSpawner>
{
    [SerializeField] private ParticleSystem _sparksTemplate;
    [SerializeField] private ParticleSystem _bloodTemplate;

    private void SpawnParticles(Vector2 position, ParticleSystem particles)
    {
        var spawnedParticles = Instantiate(particles, position, Quaternion.identity);
        spawnedParticles.Play();
    }

    [ClientRpc]
    public void SpawnSparksClientRpc(Vector2 position)
    {
        SpawnParticles(position, _sparksTemplate);
    }

    [ClientRpc]
    public void SpawnBloodClientRpc(Vector2 position)
    {
        SpawnParticles(position, _bloodTemplate);
    }
}