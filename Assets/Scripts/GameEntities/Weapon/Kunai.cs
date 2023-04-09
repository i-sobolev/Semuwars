using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class Kunai : Weapon
{
    public static event UnityAction<Vector2> Crossed;

    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _collider;
    [Space]
    [SerializeField] private float _speed;

    private int _bouncesCount = 5;

    private void FixedUpdate()
    {
        _rigidBody.rotation += _rigidBody.velocity.x < 0 ? 0.1f : -0.1f;
        _spriteRenderer.flipX = _rigidBody.velocity.x < 0 ? true : false;
    }

    private void Start()
    {
        _bouncesCount = Random.Range(1, 5);
        _rigidBody.AddForce(Owner.Movement.ViewDiretion * _speed, ForceMode2D.Impulse);
        StartCoroutine(DisableCollider());
    }

    private IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(0.05f);
        _collider.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Ground>(out _))
        {
            _rigidBody.AddForceAtPosition(Vector2.up * 10, Vector2.right * 15);
            _bouncesCount--;

            if (_bouncesCount <= 0)
            {
                _rigidBody.simulated = false;
                _animator.Play("Stucked");
            }
        }

        if (other.gameObject.TryGetComponent<Kunai>(out var kunai))
        {
            _rigidBody.velocity = (transform.position - kunai.transform.position) * 10;

            ParticlesSpawner.Instance.SpawnSparks((transform.position + kunai.transform.position) / 2);
        }

        if (other.gameObject.TryGetComponent<Character>(out _))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Sword>(out _))
        {
            _rigidBody.velocity = new Vector2(-_rigidBody.velocity.x, _rigidBody.velocity.y);

            ParticlesSpawner.Instance.SpawnSparks(transform.position);
        }
    }
}