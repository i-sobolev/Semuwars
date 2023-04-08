using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [Space]
    [SerializeField] private Transform _player1;
    [SerializeField] private Transform _player2;

    private Vector3 _cameraTargetPosition;
    
    void Update()
    {
        _cameraTargetPosition = new Vector2((_player1.position.x + _player2.position.x) / 2, transform.position.y);
        
        transform.position = Vector2.Lerp(transform.position, _cameraTargetPosition, 0.015f);

        float minCameraSize = 11.3f;
        float maxCameraSize = 15f;

        var distanceBtwPlayers = Vector2.Distance(_player1.position, _player2.position);
        var targetCameraSize = Mathf.Lerp(minCameraSize, maxCameraSize, Mathf.InverseLerp(15, 30, distanceBtwPlayers));

        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, targetCameraSize, 0.015f);
    }
}
