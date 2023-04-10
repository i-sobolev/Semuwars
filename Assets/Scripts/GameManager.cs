using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private NetworkObject _characterTemplate;

    private int _playersCounter = 1;

    private void Awake()
    {
        Player.PlayerConnected += OnPlayerSpawnedServerRpc;
    }

    [ServerRpc]
    private void OnPlayerSpawnedServerRpc(NetworkBehaviourReference playerReference)
    {
        var newCharacter = Instantiate(_characterTemplate, RespawnPositionsHelper.Instance.GetRespawnPosition(), Quaternion.identity);

        newCharacter.Spawn();

        var reerere = new NetworkObjectReference(newCharacter);

        playerReference.TryGet<Player>(out var player);

        player.SetCharacterClientRpc(reerere);
        player.Name += _playersCounter.ToString();
        
        _playersCounter++;
    }
}