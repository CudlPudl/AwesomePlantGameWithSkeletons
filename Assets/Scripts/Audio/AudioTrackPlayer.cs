using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrackPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;

    public void PlayTrack(AudioClip clip)
    {
        _source.clip = clip;
    }

    public void SetVolume(float volume)
    {
        _source.volume = volume;
    }
}
