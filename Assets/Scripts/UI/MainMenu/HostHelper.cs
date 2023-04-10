using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class HostHelper : MonoBehaviour
{
    [SerializeField] private MatchManager _matchManager;
    [SerializeField] private UnityTransport _unityTransport;
    [Space]
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _hostButton;
    [Header("Duration")]
    [SerializeField] private TextMeshProUGUI _durationText;
    [SerializeField] private Button _addTimeButton;
    [SerializeField] private Button _removeTimeButton;

    private int _matchDuration = 120;

    private void Start()
    {
        _inputField.onValueChanged.AddListener(OnInputFieldValueChanged);

        _hostButton.onClick.AddListener(Host);

        _addTimeButton.onClick.AddListener(AddTime);
        _removeTimeButton.onClick.AddListener(RemoveTime);

        _durationText.text = _matchDuration.ToString();
    }

    private void AddTime()
    {
        _matchDuration = Mathf.Clamp(_matchDuration + 30, 30, 600);
        _durationText.text = _matchDuration.ToString();
    }

    private void RemoveTime()
    {
        _matchDuration = Mathf.Clamp(_matchDuration - 30, 30, 600);
        _durationText.text = _matchDuration.ToString();
    }

    private void Host()
    {
        NetworkManager.Singleton.StartHost();

        _matchManager.StartMatch(_matchDuration);
    }

    private void OnInputFieldValueChanged(string newValue)
    {
        _unityTransport.ConnectionData.Address = newValue;
    }
}
