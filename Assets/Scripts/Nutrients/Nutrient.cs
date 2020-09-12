﻿using UnityEngine;
using UnityEngine.Events;

public class Nutrient : MonoBehaviour
{
    public enum NutrientType
    {
        Pee = 0,
        Poo = 1,
        Coffee = 2,
        Water = 3
    }

    public class NutrientEvent : UnityEvent<(NutrientType type, int Amount)> { }

    public Nutrient(NutrientType type)
    {
        _type = type;
    }

    public NutrientEvent OnNutrientsChanged = new NutrientEvent();
    private NutrientType _type;
    public NutrientType GetNutrientType() => _type;
    private int _amount = 100;

    public bool HasNutrient(int cost) => _amount > cost;
    public bool PayNutrient(int cost)
    {
        if (!HasNutrient(cost)) return false;
        _amount -= cost;
        OnNutrientsChanged.Invoke((_type, _amount));
        return true;
    }

    public void AddNutrients(int amount)
    {
        _amount += amount;
        OnNutrientsChanged.Invoke((_type, _amount));
    }
}
