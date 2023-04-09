using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayersCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    
    private List<Character> _characters = new();

    private void Start()
    {
        _characters = FindObjectsOfType<Character>().ToList();
    }

    private void OnEnable()
    {
        Player.PlayerSpawned += OnPlayerSpawned;
        Player.PlayerDestroyed += OnPlayerDestroyed;
    }

    private void OnDisable()
    {
        Player.PlayerSpawned -= OnPlayerSpawned;
        Player.PlayerDestroyed -= OnPlayerDestroyed;
    }

    private void OnPlayerSpawned(Player player) => _characters.Add(player.Character);
    private void OnPlayerDestroyed(Player player) => _characters.Remove(player.Character);


    private void Update()
    {
        if (_characters.Count == 0)
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

        foreach (var player in _characters)
            positionsSum += player.transform.position;

        return positionsSum / _characters.Count;
    }

    private float FindMaxDistanceBtwPlayer()
    {
        var maxDistance = 0f;

        foreach (var player1 in _characters)
        {
            foreach (var player2 in _characters)
            {
                var distanceBtwPlayer = Vector2.Distance(player1.transform.position, player2.transform.position);

                if (distanceBtwPlayer > maxDistance)
                    maxDistance = distanceBtwPlayer;
            }
        }

        return maxDistance;
    }
}
