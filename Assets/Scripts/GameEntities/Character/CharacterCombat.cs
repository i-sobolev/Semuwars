using UnityEngine;
using UnityEngine.Events;

public class CharacterCombat : MonoBehaviour
{
    public event UnityAction KunaiThrown;
    public event UnityAction AttackedMelee;

    [Header("Kunai")]
    [SerializeField] private Kunai _kunaiTemplate;
    [SerializeField] private float _kunaiCooldownTime;
    [SerializeField] private Vector2 _kunaiSpawnPosition;
    [Header("MeleeAttack")]
    [SerializeField] private Sword _sword;
    [SerializeField] private float _swordCooldownTime;

    private Cooldown _kunaiCooldown;
    private Cooldown _swordCooldown;

    private void Awake()
    {
        _kunaiCooldown = new(_kunaiCooldownTime);
        _swordCooldown = new(_swordCooldownTime);
    }

    public void ThrowKunai(Character owner)
    {
        if (_kunaiCooldown.Enabled)
            return;

        var kunai = Instantiate(_kunaiTemplate, (Vector2)transform.position + _kunaiSpawnPosition * owner.Movement.ViewDiretion.x, Quaternion.identity);
        
        kunai.Owner = owner;

        _kunaiCooldown.Start();
        KunaiThrown?.Invoke();
    }

    public void MeleeAttack(Character owner)
    {
        if (_swordCooldown.Enabled)
            return;

        _sword.Owner = owner;

        _sword.Attack();

        _swordCooldown.Start();
        AttackedMelee?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + _kunaiSpawnPosition);
    }
}