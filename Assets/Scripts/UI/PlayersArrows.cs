using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayersArrows : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Image _arrowTemplate;
    [Space]
    [SerializeField] private Vector2 _arrowOffset;
    [SerializeField] private Color _enemyArrowColor;

    private List<Arrow> _arrows = new();

    private void Awake()
    {
        RefreshArrowsList();

        NetworkManager.Singleton.OnClientConnectedCallback += (_) => RefreshArrowsList();
    }

    public async void RefreshArrowsList()
    {
        await System.Threading.Tasks.Task.Delay(1000);

        foreach (var arrow in _arrows)
            Destroy(arrow.ArrowImage);

        _arrows.Clear();

        foreach (var player in FindObjectsOfType<Player>())
            OnPlayerSpawned(player);
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

    private void OnPlayerSpawned(Player player)
    {
        var newArrow = new Arrow
        {
            Player = player,
            ArrowImage = Instantiate(_arrowTemplate, _arrowTemplate.transform.parent)
        };

        _arrows.Add(newArrow);

        if (!player.IsOwner)
            newArrow.ArrowImage.color = _enemyArrowColor;
    }

    private void OnPlayerDestroyed(Player player)
    {
        _arrows.Remove(_arrows.Find(x => x.Player));
    }

    private void Update()
    {
        foreach (var arrow in _arrows)
        {
            if (arrow.Player != null)
            {
                arrow.ArrowImage.rectTransform.anchoredPosition = _camera.WorldToScreenPoint((Vector2)arrow.Player.Character.transform.position + _arrowOffset);
            }
        }
    }

    [System.Serializable]
    private struct Arrow
    {
        public Player Player;
        public Image ArrowImage;
    }
}
