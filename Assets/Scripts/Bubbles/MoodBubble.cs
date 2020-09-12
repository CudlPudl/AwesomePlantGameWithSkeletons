using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodBubble : MonoBehaviour
{
    [Serializable]
    public struct PersonalityIconPair
    {
        public Plant.Personality Personality;
        public Sprite Sprite;
    }

    [SerializeField]
    private List<PersonalityIconPair> _iconPairs = new List<PersonalityIconPair>();

    [SerializeField]
    private RectTransform _transform;
    public RectTransform GetRectTransform() => _transform;
    [SerializeField]
    private Image _image;

    public void SetIcon(Plant.Personality personality)
    {
        for (var i = 0; i < _iconPairs.Count; ++i)
        {
            if (_iconPairs[i].Personality == personality)
            {
                _image.sprite = _iconPairs[i].Sprite;
                return;
            }
        }
    }
}
