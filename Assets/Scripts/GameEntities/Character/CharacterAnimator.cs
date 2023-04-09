using UnityEngine;
using UnityEngine.Events;

public class CharacterAnimator : MonoBehaviour
{
    private const string _jumped = "Jumped";
    private const string _kunaiThrown = "KunaiThrow";
    private const string _meleeAttack = "MeleeAttack";

    private const string _grounded = "Grounded";
    private const string _moving = "Moving";

    private const string _speed = "Speed";

    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private CharacterCombat _characterCombat;    

    private void Start()
    {
        _characterMovement.Jumped += PlayJump;

        _characterCombat.AttackedMelee += PlayMeleeAttack;
        _characterCombat.KunaiThrown += PlayKunaiThrow;
    }

    private void Update()
    {
        SetGrounded(_characterMovement.IsGrounded.Value);
        SetMoving(_characterMovement.IsMoving.Value);
        SetSpeed(_characterMovement.Speed.Value);
    }


    public void PlayKunaiThrow()
    {
        _animator.SetTrigger(_kunaiThrown);
    }

    public void PlayMeleeAttack()
    {
        _animator.SetTrigger(_meleeAttack);
    }

    public void PlayJump()
    {
        _animator.SetTrigger(_jumped);
    }

    public void SetGrounded(bool value)
    {
        _animator.SetBool(_grounded, value);
    }

    public void SetMoving(bool value)
    {
        _animator.SetBool(_moving, value);
    }

    public void SetSpeed(float value)
    {
        _animator.SetFloat(_speed, value);
    }
}