using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientCounter : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text _text;

    public void OnNutrientsChanged((Nutrient.NutrientType type, int amount)tuple)
    {
        _text.text = tuple.amount.ToString();
    }
}
