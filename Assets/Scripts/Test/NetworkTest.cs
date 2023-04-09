using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkTest : MonoBehaviour
{
    [SerializeField] private Button _host;
    [SerializeField] private Button _client;

    private void Start()
    {
        _host.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        _client.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
    }
}
