using UnityEngine;

public class RespawnPositionsHelper : Singleton<RespawnPositionsHelper>
{
    [SerializeField] private RespawnPositions[] _positions;

    [System.Serializable]
    struct RespawnPositions
    {
        public Vector2 Start;
        public Vector2 End;
    }

    public Vector2 GetRespawnPosition()
    {
        var randomPositions = _positions.PickRandom();

        return Vector2.Lerp(randomPositions.Start, randomPositions.End, Random.Range(0, 1f));
    }

    private void OnDrawGizmos()
    {
        if (_positions == null)
            return;

        foreach (var positions in _positions)
            Gizmos.DrawLine(positions.Start, positions.End);
    }
}