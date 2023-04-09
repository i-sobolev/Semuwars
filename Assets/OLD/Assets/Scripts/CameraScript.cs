using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [Space]
    [SerializeField] private List<Player> _players;

    private void Awake()
    {
        Player.PlayerSpawned += OnPlayerSpawned;
        Player.PlayerDestroyed += OnPlayerDestroyed;
    }

    private void OnPlayerSpawned(Player player) => _players.Add(player);
    private void OnPlayerDestroyed(Player player) => _players.Remove(player);


    private void Update()
    {
        if (_players.Count == 0)
            return;

        var cameraTargetPosition = new Vector2(CalculatePlayersMedianPoint().x, transform.position.y);
        
        transform.position = Vector2.Lerp(transform.position, cameraTargetPosition, 0.015f);

        float minCameraSize = 11.3f;
        float maxCameraSize = 15f;

        var distanceBtwPlayers = FindMaxDistanceBtwPlayer();
        var targetCameraSize = Mathf.Lerp(minCameraSize, maxCameraSize, Mathf.InverseLerp(15, 30, distanceBtwPlayers));

        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, targetCameraSize, 0.015f);
    }

    private Vector2 CalculatePlayersMedianPoint()
    {
        var positionsSum = Vector3.zero;

        foreach (var player in _players)
            positionsSum += player.transform.position;

        return positionsSum / _players.Count;
    }

    private float FindMaxDistanceBtwPlayer()
    {
        var maxDistance = 0f;

        foreach (var player1 in _players)
        {
            foreach (var player2 in _players)
            {
                var distanceBtwPlayer = Vector2.Distance(player1.transform.position, player2.transform.position);

                if (distanceBtwPlayer > maxDistance)
                    maxDistance = distanceBtwPlayer;
            }
        }

        return maxDistance;
    }
}
