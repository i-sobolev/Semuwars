using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class ConnetionHelper : MonoBehaviour
{
    [SerializeField] private UnityTransport _unityTransport;
    [Space]
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _connectButton;

    private void Start()
    {
        _inputField.onValueChanged.AddListener(OnInputFieldValueChanged);

        _connectButton.onClick.AddListener(Connect);
    }

    private void Connect() => NetworkManager.Singleton.StartClient();

    private void OnInputFieldValueChanged(string newValue)
    {
        _unityTransport.ConnectionData.Address = newValue;
    }
}