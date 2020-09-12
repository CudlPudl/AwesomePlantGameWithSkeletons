using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPot : MonoBehaviour
{
    [SerializeField]
    private Plant _prefab;
    [SerializeField]
    private Transform _root;
    private Plant _currentPlant;
    private bool _hasPlant = false;

    private void SpawnPlant()
    {
        _currentPlant = Instantiate(_prefab, _root);
        _currentPlant.SetPersonality(GetRandomPersonality());
        _currentPlant.OnPickEvent.AddListener(OnPlantDestroyed);
        _hasPlant = true;
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
    }
}
