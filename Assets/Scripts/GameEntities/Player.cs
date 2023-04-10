#pragma warning disable

using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;

public class Player : NetworkBehaviour
{
    public static event UnityAction<NetworkBehaviourReference> PlayerConnected;

    public static event UnityAction<Player> PlayerSpawned;
    public static event UnityAction<Player> PlayerDestroyed;

    public event UnityAction<Character, Player> CharacterKilled;

    private Character _character;
    public Character Character => _character;

    public NetworkVariable<Vector2> CharacterPosition = new();

    public int Score { get; set; } = 0;
    public string Name { get; set; } = "Player ";

    private void Start()
    {
        PlayerConnected?.Invoke(new NetworkBehaviourReference(this));

        PlayerSpawned?.Invoke(this);
    }

    private void OnDestroy()
    {
        base.OnDestroy();

        PlayerDestroyed?.Invoke(this);
    }

    [ClientRpc]
    public void SetCharacterClientRpc(NetworkObjectReference characterReference)
    {
        characterReference.TryGet(out var characterObject);

        _character = characterObject.GetComponent<Character>();

        _character.Killed += OnCharacterKilled;
    }

    private void OnCharacterKilled(Character killer)
    {
        CharacterKilled?.Invoke(killer, this);
    }

    private void Update()
    {
        if (!IsOwner || !_character)
            return;

        //float inputHorizontal = Input.GetAxisRaw("Horizontal");
        //_character.Movement.Move(inputHorizontal);

        //if (Input.GetKeyDown(KeyCode.W))
        //    _character.Movement.Jump();

        //if (Input.GetMouseButtonDown(0))
        //    _character.Combat.MeleeAttack(_character);

        //if (Input.GetMouseButtonDown(1))
        //    _character.Combat.ThrowKunai(_character);

        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        MoveServerRpc(inputHorizontal);

        if (Input.GetKeyDown(KeyCode.W))
            JumpServerRpc();

        if (Input.GetMouseButtonDown(0))
            MeleeAttackServerRpc();

        if (Input.GetMouseButtonDown(1))
            ThrowKunaiServerRpc();

        RefreshCharacterPositionServerRpc();
    }

    [ServerRpc]
    private void RefreshCharacterPositionServerRpc()
    {
        CharacterPosition.Value = _character.transform.position;
    }

    [ServerRpc]
    private void MoveServerRpc(float inputHorizontal)
    {
        _character.Movement.Move(inputHorizontal);
    }

    [ServerRpc]
    private void JumpServerRpc()
    {
        _character.Movement.Jump();
    }

    [ServerRpc]
    private void MeleeAttackServerRpc()
    {
        _character.Combat.MeleeAttack(_character);
    }

    [ServerRpc]
    private void ThrowKunaiServerRpc()
    {
        _character.Combat.ThrowKunai(_character);
    }
}
