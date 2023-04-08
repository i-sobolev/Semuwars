using UnityEngine;

public class Character : MonoBehaviour
{
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
        _characterMovement.Jumped += () =>
        {
            var particles = Instantiate(_jumpParticles, _jumpParticles.transform.parent);
            particles.Play();
        };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Weapon>(out var weapon))
        {
            if (weapon is not Sword || weapon.Owner != this)
                Respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Weapon>(out var weapon))
        {
            if (weapon is not Sword || weapon.Owner != this)
                Respawn();
        }
    }

    private void Update()
    {
        _spriteRenderer.flipX = Movement.ViewDiretion == Vector2.left;
    }

    private void Respawn()
    {
        ParticlesSpawner.Instance.SpawnBlood(transform.position);

        var respawnPosition = RespawnPositionsHelper.Instance.GetRespawnPosition();

        transform.position = respawnPosition;

        Movement.ResetVelocity();
    }
}
