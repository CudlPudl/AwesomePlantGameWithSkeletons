using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [System.Serializable]
    public struct CameraPositions
    {
        public string Name;
        public Cinemachine.CinemachineVirtualCamera Camera;
    }

    [SerializeField]
    private List<CameraPositions> _cameraPositions;

    private void Start()
    {
        if (_cameraPositions.Count <= 0) return;
        SetCameraPosition(0);
    }

    public void SetCameraPosition(string Name)
    {
        for (var i = 0; i < _cameraPositions.Count; ++i)
        {
            var pos = _cameraPositions[i];
            pos.Camera.gameObject.SetActive(pos.Name == Name);
        }
    }

    private void SetCameraPosition(int index)
    {
        for (var i = 0; i < _cameraPositions.Count; ++i)
        {
            var pos = _cameraPositions[i];
            pos.Camera.gameObject.SetActive(i == index);
        }
    }

}
