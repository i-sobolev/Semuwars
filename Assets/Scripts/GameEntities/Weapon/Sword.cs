using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Sword : Weapon
{
    public static event UnityAction SwordsCrossed;

    [SerializeField] private BoxCollider2D _collider;

    private Vector2 _baseColliderOffset = Vector2.zero;
    private float _notGroundedOffsetY = 0.08f;

    private void Start()
    {
        _baseColliderOffset = _collider.offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Sword>(out var sword))
        {
            Owner.Movement.TossInOppositeDirection();

            ParticlesSpawner.Instance.SpawnSparks((transform.position + sword.transform.position) / 2);
        }
    }

    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        _collider.enabled = true;

        FixColliderOffset();

        var framesCount = 10;

        for(int i = 0; i < framesCount; i++)
            yield return null;

        _collider.enabled = false;
    }

    private void FixColliderOffset()
    {
        var characterMovement = Owner.Movement;

        _collider.offset = _baseColliderOffset * characterMovement.ViewDiretion.x;

        if (!characterMovement.IsGrounded)
            _collider.offset = new Vector2(_collider.offset.x, _notGroundedOffsetY);
    }
}