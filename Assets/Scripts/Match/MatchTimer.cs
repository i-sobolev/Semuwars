using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class MatchTimer : NetworkBehaviour
{
    public event UnityAction Expired;

    [SerializeField] private TextMeshProUGUI _text;

    private NetworkVariable<float> _time = new (0);

    private bool _started = false;

    public void SetAndStart(int time)
    {
        _time.Value = time;
        _started = true;
    }

    private void Update()
    {
        if (IsHost && _started)
        {
            _time.Value -= Time.deltaTime;

            if (_time.Value <= 0)
            {
                Expired?.Invoke();
                _started = false;
            }
        }

        _text.text = ((int)Mathf.Clamp(_time.Value, 0, 600)).ToString();
    }
}