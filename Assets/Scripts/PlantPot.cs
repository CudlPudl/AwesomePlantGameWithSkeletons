﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlantPot : MonoBehaviour
{
    [SerializeField]
    private Plant _prefab;
    [SerializeField]
    private Transform _root;
    private Plant _currentPlant;
    private bool _hasPlant = false;

    public UnityEvent OnPlantSpawn = new UnityEvent();
    public UnityEvent OnPlantDie = new UnityEvent();

    private void SpawnPlant()
    {
        _currentPlant = Instantiate(_prefab, _root);
        _currentPlant.SetPersonality(GetRandomPersonality());
        _currentPlant.OnPickEvent.AddListener(OnPlantDestroyed);
        _currentPlant.Pot = this;
        _hasPlant = true;
        OnPlantSpawn.Invoke();
    }

    private Plant.Personality GetRandomPersonality()
    {
        var types = Enum.GetNames(typeof(Plant.Personality));
        var count = types.Length;
        var rnd = UnityEngine.Random.value;
        for (var i = 0; i < types.Length; ++i)
        {
            var comparison = rnd < 1f / (float)count * (float)(i + 1);
            if (comparison)
            {
                return (Plant.Personality)Enum.Parse(typeof(Plant.Personality), types[i]);
            }
        }
        return Plant.Personality.Cute;
    }

    private void OnPlantDestroyed(Plant plant)
    {
        _currentPlant = null;
        _hasPlant = false;
        OnPlantDie.Invoke();
    }

    public void AddPlant()
    {
        if (_hasPlant)
        {
            if (_currentPlant.IsFullyGrown())
            {
                RemovePlant();
            }
            return;
        }
        SpawnPlant();
    }

    public void RemovePlant()
    {
        _currentPlant.Reset();
        Destroy(_currentPlant.gameObject);
        _hasPlant = false;
        OnPlantDie.Invoke();
    }
}
