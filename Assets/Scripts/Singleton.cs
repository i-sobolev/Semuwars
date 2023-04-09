#pragma warning disable
using Unity.Netcode;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get => _instance ??= FindObjectOfType<T>();
    }

    protected virtual void Awake()
    {
        _instance = this as T;
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }
}

public abstract class NetworkSingleton<T> : NetworkBehaviour where T : NetworkBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get => _instance ??= FindObjectOfType<T>();
    }

    protected virtual void Awake()
    {
        _instance = this as T;
    }

    protected virtual void OnDestroy()
    {
        base.OnDestroy();

        _instance = null;
    }
}