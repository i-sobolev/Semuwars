using Unity.Netcode;
using UnityEngine;

public class AudioPlayer : NetworkSingleton<AudioPlayer>
{
    [SerializeField] private AudioClip _characterKill;
    [SerializeField] private AudioClip _characterLanding;
    
    [SerializeField] private AudioClip _kunaiGroundHit;
    [SerializeField] private AudioClip _kunaiThrow;
    [SerializeField] private AudioClip _kunaiStuck;

    [SerializeField] private AudioClip _swordHit;
    [SerializeField] private AudioClip _swordCross;
    [Space]
    [SerializeField] private AudioSource _audioSource;

    private void PlaySFX(AudioClip audioClip)
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(audioClip);
    }

    [ClientRpc]
    public void PlayCharacterKillClientRpc() => PlaySFX(_characterKill);

    [ClientRpc]
    public void PlayCharacterLandingClientRpc() => PlaySFX(_characterLanding);

    [ClientRpc]
    public void PlayKunaiGroundHitClientRpc() => PlaySFX(_kunaiGroundHit);

    [ClientRpc]
    public void PlayKunaiThrowClientRpc() => PlaySFX(_kunaiThrow);

    [ClientRpc]
    public void PlayKunaiStuckClientRpc() => PlaySFX(_kunaiStuck);

    [ClientRpc]
    public void PlaySwordHitClientRpc() => PlaySFX(_swordHit);

    [ClientRpc]
    public void PlaySwordCrossClientRpc() => PlaySFX(_swordCross);
}
