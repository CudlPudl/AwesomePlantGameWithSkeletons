using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activity : MonoBehaviour
{
    [SerializeField]
    private Nutrient.NutrientType _nutrientType;
    [SerializeField]
    private float _holdTimeForNutrient = 1f;
    [SerializeField]
    private UnityEvent OnStartHold = new UnityEvent();
    [SerializeField]
    private UnityEvent OnHoldEnd = new UnityEvent();

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
        OnStartHold.Invoke();
    }

    public void EndHold()
    {
        OnHoldEnd.Invoke();
        _isHold = false;
    }

}
