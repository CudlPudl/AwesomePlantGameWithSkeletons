using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantWitherVisualization : MonoBehaviour
{
    private static int c_ColorProperty = Shader.PropertyToID("_Color");

    [SerializeField]
    private SkinnedMeshRenderer _renderer;
    [SerializeField]
    private Color _witherColor;
    [SerializeField]
    private Vector3 _witherScale = new Vector3(0.5f, 0.9f, 0.5f);

    private static Vector3 s_normalScale = new Vector3(1f, 1f, 1f);
    private List<Color> _originalColors = new List<Color>();

    public void Awake()
    {
        foreach (var mat in _renderer.materials)
        {
            _originalColors.Add(mat.GetColor(c_ColorProperty));
        }
    }

    public void SetWitherLevel(float normalized)
    {
        var mats = _renderer.materials;
        for (var i = 0; i < mats.Length; ++i)
        {
            mats[i].SetColor(c_ColorProperty, Color.Lerp(_originalColors[i], _witherColor, normalized));
        }
        _renderer.transform.localScale = Vector3.Lerp(s_normalScale, _witherScale, normalized);
    }
}
