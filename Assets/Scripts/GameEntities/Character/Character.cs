using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class Character : NetworkBehaviour
{
    public event UnityAction<Character> Killed;

    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private CharacterCombat _characterCombat;
    [Space]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [Header("Particles")]
    [SerializeField] private ParticleSystem _jumpParticles;

    public CharacterMovement Movement => _characterMovement;
    public CharacterCombat Combat => _characterCombat;

    private void Start()
    {
        _characterMovement.Jumped += SpawnJumpParticlesClientRpc;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Weapon>(out var weapon))
        {
            if (weapon is not Sword || weapon.Owner != this)
            {
                Respawn();

                Killed?.Invoke(weapon.Owner);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Weapon>(out var weapon))
        {
            if (weapon is not Sword || weapon.Owner != this)
            {
                Respawn();

                Killed?.Invoke(weapon.Owner);
            }
        }
    }

    private void Update()
    {
        _spriteRenderer.flipX = Movement.ViewDiretion.Value == Vector2.left;
    }

    private void Respawn()
    {
        ParticlesSpawner.Instance.SpawnBloodClientRpc(transform.position);

        var respawnPosition = RespawnPositionsHelper.Instance.GetRespawnPosition();

        transform.position = respawnPosition;

        Movement.ResetVelocity();
    }

    [ClientRpc]
    private void SpawnJumpParticlesClientRpc()
    {
        var particles = Instantiate(_jumpParticles, _jumpParticles.transform.parent);
        particles.Play();
    }
}