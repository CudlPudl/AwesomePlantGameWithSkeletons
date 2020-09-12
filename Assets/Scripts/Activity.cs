using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour
{
    [SerializeField]
    private Nutrient.NutrientType _nutrientType;
    [SerializeField]
    private float _holdTimeForNutrient = 1f;

    private float _startTime;
    private bool _isHold;

    public void Update()
    {
        if (!_isHold) return;
        if (_startTime + _holdTimeForNutrient < Time.time)
        {
            _startTime = Time.time;
            NutrientsManager.Instance.AddToStorage(_nutrientType, 1);
        }
    }

    public void StartHold()
    {
        _isHold = true;
        _startTime = Time.time;
    }

    public void EndHold()
    {
        _isHold = false;
    }

}
