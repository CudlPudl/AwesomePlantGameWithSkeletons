using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer: MonoBehaviour
{
    [SerializeField]
    private AudioClip _clip;
    [SerializeField, Range(0f, 1f)]
    private float _volume = 1f;
    [SerializeField]
    private bool _loop;
    private AudioSource _source;

    public void Play()
    {
        //Prevent from playing same track multiple times if loop;
        if (_loop && _source != null) return;
        _source = AudioClipManager.Instance.PlayClip(_clip, _loop, _volume);
    }

    public void Stop()
    {
        if (_source != null && _loop)
        {
            _source.Stop();
            _source = null;
        }
    }

    public void OnDestroy()
    {
        if (_source != null && _loop)
        {
            _source.Stop();
        }
    }
}
