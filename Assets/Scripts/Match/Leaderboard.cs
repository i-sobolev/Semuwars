using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class Leaderboard : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void Set(IEnumerable<Player> players)
    {
        players.OrderBy(x => x.Score).ToArray();

        var result = string.Empty;

        foreach (var player in players)
        {
            result += $"{player.Name} - {player.Score}\n";
        }

        _text.text = result;
    }
}