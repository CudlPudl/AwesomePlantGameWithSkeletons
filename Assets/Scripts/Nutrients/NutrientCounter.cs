using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NutrientCounter : MonoBehaviour
{
    [Serializable]
    public struct NutrientIconPair
    {
        public Nutrient.NutrientType Type;
        public Sprite Sprite;
    }

    [SerializeField]
    private TMPro.TMP_Text _text;
    [SerializeField]
    private Image _image;

    [SerializeField]
    private List<NutrientIconPair> _iconPairs = new List<NutrientIconPair>();

    public void OnNutrientsChanged((Nutrient.NutrientType type, int amount)tuple)
    {
        _text.text = tuple.amount.ToString();
        for (var i = 0; i < _iconPairs.Count; ++i)
        {
            if (_iconPairs[i].Type == tuple.type)
            {
                _image.sprite = _iconPairs[i].Sprite;
                return;
            }
        }
    }
}
