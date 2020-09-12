using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScroller : MonoBehaviour
{
    [SerializeField]
    private CinemachineDollyCart _cart;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private float _speedMultiplier = 0.1f;

    private Vector3 _touchStart;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _touchStart = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = _touchStart - Input.mousePosition;
            direction /= Screen.currentResolution.width;
            _cart.m_Position += direction.x * _speedMultiplier;
        }
    }
}
