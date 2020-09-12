using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class HitScanner : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    private RaycastHit _raycastHit;
    private ITapable _currentTapable;

    private void Update()
    {
        //TODO: store a list of colliders here and use them as a key to dictionary to find the correct object
        //      to avoid using GetComponent constantly to find the Tapable object.

        if (Input.GetMouseButtonDown(0))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out _raycastHit)) return;
            _currentTapable = _raycastHit.collider.GetComponent<ITapable>();
            _currentTapable?.IsTapped();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_currentTapable != null)
            {
                _currentTapable.IsReleased();
                _currentTapable = null;
            }
        }
    }
}
