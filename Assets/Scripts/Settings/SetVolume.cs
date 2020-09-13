using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    public void Awake()
    {
        _slider.minValue = 0f;
        _slider.maxValue = 1f;
        _slider.onValueChanged.AddListener(OnValueChanged);
        _slider.value = GameManager.Instance.GetGlobalVolume();
    }

    public void OnValueChanged(float value)
    {
        GameManager.Instance.SetGlobalVolume(value);
    }
}
