using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientsManager : Singleton<NutrientsManager>
{
    [SerializeField]
    private NutrientCounter _prefab;
    [SerializeField]
    private RectTransform _counterRoot;

    private List<Nutrient> _nutrients = new List<Nutrient>();

    private void Start()
    {
        CreateNutrients();
    }

    private void CreateNutrients()
    {
        foreach (Nutrient.NutrientType type in Enum.GetValues(typeof(Nutrient.NutrientType)))
        {
            CreateNutrient(type);   
        }
    }

    private void CreateNutrient(Nutrient.NutrientType type)
    {
        var nutrient = new Nutrient(type);
        _nutrients.Add(nutrient);
        var counter = Instantiate(_prefab, _counterRoot);
        nutrient.OnNutrientsChanged.AddListener(counter.OnNutrientsChanged);
    }

    public bool Purchase(Nutrient.NutrientType type, int amount)
    {
        foreach (var nutrient in _nutrients)
        {
            if (nutrient.GetNutrientType() == type) return nutrient.PayNutrient(amount);
        }
        return false;
    }

    public void AddNutrients(Nutrient.NutrientType type, int amount)
    {
        foreach (var nutrient in _nutrients)
        {
            if (nutrient.GetNutrientType() == type)
            {
                nutrient.AddNutrients(amount);
                return;
            }
        }
    }
}
