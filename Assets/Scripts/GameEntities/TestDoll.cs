using UnityEngine;

public class TestDoll : MonoBehaviour
{
    [SerializeField] private Character _character;

    private Cooldown _kunaiCooldown = new Cooldown(2);
    private Cooldown _swordCooldown = new Cooldown(3);
    private Cooldown _jumpCooldown = new Cooldown(1);

    [SerializeField] private bool _syncWithPlayer;

    private void Start()
    {
        _kunaiCooldown.Start();
        _swordCooldown.Start();
        _jumpCooldown.Start();
    }

    private void Update()
    {
        if (!_syncWithPlayer)
        {
            if (!_jumpCooldown.Enabled)
            {
                _character.Movement.Jump();
                _jumpCooldown.Start();
            }
            if (!_swordCooldown.Enabled)
            {
                _character.Combat.MeleeAttack(_character);
                _swordCooldown.Start();
            }

            if (!_kunaiCooldown.Enabled)
            {
                _character.Combat.ThrowKunai(_character);
                _kunaiCooldown.Start();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                _character.Combat.MeleeAttack(_character);
            }

            if (Input.GetMouseButtonDown(1))
            {
                _character.Combat.ThrowKunai(_character);
            }
        }
    }

}
