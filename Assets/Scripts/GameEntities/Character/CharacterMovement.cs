using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : NetworkBehaviour
{
    public event UnityAction Jumped;

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidBody;
    [Header("Parameters")]
    [Header("Running")]
    [SerializeField] private float _acceleration = 1500;
    [SerializeField] private float _maxSpeed = 15;
    [Header("Jumping")]
    [SerializeField] private float _jumpForce = 68000;
    [SerializeField] private int _defaultMultipleJumpsCount = 2;

    private int _jumpsAvailable = 0;

    public NetworkVariable<bool> IsGrounded { get; private set; } = new (false);
    public NetworkVariable<bool> IsMoving { get; private set; } = new (false);
    public NetworkVariable<float> Speed => new (Mathf.Abs(_rigidBody.velocity.x));

    public NetworkVariable<Vector2> ViewDiretion { get; private set; } = new (Vector2.right);

    private void Start()
    {
        _jumpsAvailable = _defaultMultipleJumpsCount;
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = new (Mathf.Clamp(_rigidBody.velocity.x, -_maxSpeed, _maxSpeed) , _rigidBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _jumpsAvailable = _defaultMultipleJumpsCount;
            IsGrounded.Value = true;

            AudioPlayer.Instance.PlayCharacterLandingClientRpc();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            IsGrounded.Value = false;
        }
    }

    public void Jump()
    {
        if (_jumpsAvailable <= 0)
            return;

        _rigidBody.AddForce(Vector2.up * _jumpForce);
        _jumpsAvailable -= 1;

        Jumped?.Invoke();
    }

    public void Move(float inputHorizontal)
    {
        var moveDirection = new Vector2 { x = inputHorizontal };

        _rigidBody.AddForce(moveDirection.normalized * _acceleration * 100 * Time.deltaTime);

        IsMoving.Value = moveDirection != Vector2.zero;

        if (inputHorizontal == 1 || inputHorizontal == -1)
            ViewDiretion.Value = moveDirection;
    }

    public void ResetVelocity() => _rigidBody.velocity = Vector2.zero;

    public void TossInOppositeDirection() => _rigidBody.velocity = new Vector2(-_rigidBody.velocity.x, _rigidBody.velocity.y + 10);
}