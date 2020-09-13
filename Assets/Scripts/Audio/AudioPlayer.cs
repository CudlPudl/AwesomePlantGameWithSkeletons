using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : Singleton<AudioPlayer>
{
    [System.Serializable]
    public class AudioTrack
    {
        public Plant.Personality Personality;
        public AudioSource Source;
        public float Influence;
        public bool IsInfluenceIncreasing;
        public void SetVolume()
        {
            Source.volume = Mathf.Clamp01(Influence);
        }
    }

    [SerializeField]
    private float _influenceChangePerSecond = 0.5f;

    [SerializeField]
    private Dictionary<Plant.Personality, bool> _addInfluence = new Dictionary<Plant.Personality, bool>();

    [SerializeField]
    private List<(Plant.Personality Personality, AudioClip Clip)> _clips;
    private List<AudioTrack> _tracks = new List<AudioTrack>();

    public void Start()
    {
        foreach(var clip in _clips)
        {
            var go = new GameObject();
            go.transform.SetParent(transform);
            var audioTrack = new AudioTrack();
            var source = go.AddComponent<AudioSource>();
            source.clip = clip.Clip;
            source.volume = 0;
            source.Play();
            audioTrack.Source = source;
            audioTrack.Personality = clip.Personality;
            audioTrack.Influence = 0;
            audioTrack.IsInfluenceIncreasing = false;
            _tracks.Add(audioTrack);
        }
    }

    public void Update()
    {
        foreach (var track in _tracks)
        {
            var difference = _influenceChangePerSecond * Time.deltaTime;
            if (track.IsInfluenceIncreasing)
            {
                difference *= -1;
            }
            track.Influence = Mathf.Clamp01(track.Influence + difference);
            track.SetVolume();
        }
    }

    public void SetInfluence(Plant.Personality personality, bool isInfluenceIncreasing)
    {
        for (var i = 0; i < _tracks.Count; ++i)
        {
            if (_tracks[i].Personality == personality)
            {
                _tracks[i].IsInfluenceIncreasing = isInfluenceIncreasing;
                return;
            }
        }
    }
}
