using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipManager : Singleton<AudioClipManager>
{
    public List<AudioSource> _sources = new List<AudioSource>();

    public AudioSource CreateAudioSource()
    {
        var go = new GameObject("AudioSource_Clip");
        go.transform.SetParent(transform);
        var source = go.AddComponent<AudioSource>();
        _sources.Add(source);
        return source;
    }

    public AudioSource GetAudioSource()
    {
        for (var i = 0; i < _sources.Count; ++i)
        {
            if (!_sources[i].isPlaying)
            {
                return _sources[i];
            }
        }
        return CreateAudioSource();
    }

    public AudioSource PlayClip(AudioClip clip, bool loop, float volume)
    {
        var source = GetAudioSource();
        source.clip = clip;
        source.loop = loop;
        source.volume = volume;
        source.Play();
        return source;
    }
}
