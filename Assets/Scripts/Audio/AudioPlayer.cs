using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : Singleton<AudioPlayer>
{
    public enum Mood
    {
        Death = -1,
        Neutral = 0,
        Happy = 1
    }

    [System.Serializable]
    public struct MoodPair
    {
        public Mood Type;
        public AudioClip Clip;
    }

    [System.Serializable]
    public class AudioTrack
    {
        public Mood Type;
        public AudioSource Source;
    }

    private float _moodScale = 1;

    [SerializeField]
    private float _influenceChangePerSecond = 0.5f;

    [SerializeField]
    private List<MoodPair> _clips;
    private List<AudioTrack> _tracks = new List<AudioTrack>();
    private List<Plant> _plantsDying = new List<Plant>();

    public void Start()
    {
        foreach(var clip in _clips)
        {
            var go = new GameObject("AudioSource_Music");
            go.transform.SetParent(transform);
            var source = go.AddComponent<AudioSource>();
            var audioTrack = new AudioTrack();
            source.clip = clip.Clip;
            source.volume = 0;
            source.loop = true;
            source.Play();
            audioTrack.Source = source;
            audioTrack.Type = clip.Type;
            _tracks.Add(audioTrack);
        }
        //Sync correct volumes.
        Update();
    }

    public void AddDeathFlag(Plant plant)
    {
        if (_plantsDying.Contains(plant)) return;
        _plantsDying.Add(plant);
    }

    public void ClearFlag(Plant plant)
    {
        if (!_plantsDying.Contains(plant)) return;
        _plantsDying.Remove(plant);
    }

    public void Update()
    {
        var direction = _plantsDying.Count > 0 ? -1 : 1;
        var delta = _influenceChangePerSecond * Time.deltaTime * direction;
        _moodScale = Mathf.Clamp(_moodScale + delta, -1, 1);

        foreach (var track in _tracks)
        {
            //var difference = _influenceChangePerSecond * Time.deltaTime;
            track.Source.volume = track.Type == Mood.Neutral ? GameManager.Instance.GetGlobalVolume() : Mathf.Clamp01(_moodScale * (float)track.Type * GameManager.Instance.GetGlobalVolume());
        }
    }

    public void Reset()
    {
        _plantsDying.Clear();
    }
}
