using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MatchManager : NetworkBehaviour
{
    [SerializeField] private MatchTimer _timer;
    [SerializeField] private Leaderboard _leaderboard;

    private List<Player> _players = new();

    private void Awake()
    {
        //if (!IsHost)
        //    return;

        Player.PlayerSpawned += OnPlayerSpawned;

        _timer.Expired += OnTimerExpiredClientRpc;
    }

    [ClientRpc]
    private void OnTimerExpiredClientRpc()
    {
        NetworkManager.Singleton.Shutdown();
    }

    public void StartMatch(int time)
    {
        _timer.SetAndStart(time);
        _leaderboard.Set(_players);
    }

    private void OnPlayerSpawned(Player player)
    {
        if (!IsHost)
            return;


        _players.Add(player);

        player.CharacterKilled += OnPlayerCharacterKilled;

        _leaderboard.Set(_players);
    }

    private void OnPlayerCharacterKilled(Character killer, Player killedPlayer)
    {
        if (!IsHost)
            return;

        var playerKiller = _players.Find(p => p.Character == killer);
            
        if (playerKiller != killedPlayer)
        {
            playerKiller.Score++;
            _leaderboard.Set(_players);
        }
    }
}