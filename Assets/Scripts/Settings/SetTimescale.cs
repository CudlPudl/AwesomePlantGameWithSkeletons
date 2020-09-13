using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTimescale : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    public void Awake()
    {
        _slider.minValue = 0.1f;
        _slider.maxValue = 10f;
        _slider.onValueChanged.AddListener(OnValueChanged);
        _slider.value = GameManager.Instance.GetGlobalTimescale();
    }

    public void OnValueChanged(float value)
    {
        GameManager.Instance.SetTimeScale(value);
    }

}
