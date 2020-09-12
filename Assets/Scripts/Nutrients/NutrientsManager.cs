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

    [Serializable]
    public struct PersonalityNutrient
    {
        public Nutrient.NutrientType Type;
        public Plant.Personality Personality;
    }

    private List<Nutrient> _nutrients = new List<Nutrient>();
    [SerializeField]
    private List<PersonalityNutrient> _personalityNutrients = new List<PersonalityNutrient>();

    public Nutrient.NutrientType GetNutrientTypeByPersonality(Plant.Personality personality)
    {
        for (var i = 0; i < _personalityNutrients.Count; ++i)
        {
            if (_personalityNutrients[i].Personality == personality) return _personalityNutrients[i].Type;
        }
        // Forgot something from the list :(
        return Nutrient.NutrientType.Poo;
    }

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
