using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempInteractionVisualization : MonoBehaviour
{
    [SerializeField]
    private Transform _root;

    public void IsTapped()
    {
        _root.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void IsReleased()
    {
        _root.localScale = Vector3.one;
    }
}
