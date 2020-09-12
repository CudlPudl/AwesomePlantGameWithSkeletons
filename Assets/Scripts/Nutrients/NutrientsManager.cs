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

    [System.Serializable]
    public class NutrientStorage
    {
        public Nutrient.NutrientType Type;
        public int Amount = 0;
        public int MaxAmount = 10;
    }

    private List<Nutrient> _nutrients = new List<Nutrient>();
    private List<NutrientStorage> _storages = new List<NutrientStorage>();
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
        CreateStorages();
    }

    #region Nutrients

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
        counter.OnNutrientsChanged((type, nutrient.GetAmount()));
    }

    public bool Purchase(Nutrient.NutrientType type, int amount)
    {
        foreach (var nutrient in _nutrients)
        {
            if (nutrient.GetNutrientType() == type) return nutrient.PayNutrient(amount);
        }
        return false;
    }

    public bool AddNutrients(Nutrient.NutrientType type, int amount)
    {
        foreach (var nutrient in _nutrients)
        {
            if (nutrient.GetNutrientType() == type)
            {
                nutrient.AddNutrients(amount);
                return true;
            }
        }
        return false;
    }

    #endregion

    #region Storages

    private void CreateStorages()
    {
        foreach (Nutrient.NutrientType type in Enum.GetValues(typeof(Nutrient.NutrientType)))
        {
            CreateStorage(type);
        }
    }

    private void CreateStorage(Nutrient.NutrientType type)
    {
        var storage = new NutrientStorage();
        storage.Type = type;
        _storages.Add(storage);
    }

    public bool AddToStorage(Nutrient.NutrientType type, int amount)
    {
        for (var i = 0; i < _storages.Count; ++i)
        {
            if (_storages[i].Type == type)
            {
                if (_storages[i].Amount >= _storages[i].MaxAmount) return false;
                _storages[i].Amount = Mathf.Min(_storages[i].Amount + amount, _storages[i].MaxAmount);
                return true;
            }
        }
        return false;
    }

    public void ClaimStorage(Nutrient.NutrientType type)
    {
        for (var i = 0; i < _storages.Count; ++i)
        {
            if (_storages[i].Type == type)
            {
                if (AddNutrients(type, _storages[i].Amount))
                {
                    _storages[i].Amount = 0;
                }
            }
        }
    }

    #endregion
}
