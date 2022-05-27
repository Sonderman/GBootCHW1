using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum AudioClips
    {
        Jump,Fall,Finish,EnemyHit,Dead,Pickup
    }
    private AudioSource _audioSource;
    public List<AudioClip> audioClips;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClips type,Vector2 position,float volume=1f)
    {
        AudioSource.PlayClipAtPoint(audioClips[(int)type],position,volume);
    }

    public void StopAudio()
    {
        _audioSource.Stop();
    }
}
