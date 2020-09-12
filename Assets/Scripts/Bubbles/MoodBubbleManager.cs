using System.Collections.Generic;
using UnityEngine;

public class MoodBubbleManager : Singleton<MoodBubbleManager>
{
    [SerializeField]
    private MoodBubble _prefab;
    [SerializeField]
    private Transform _container;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private RectTransform _canvasRect;

    private Dictionary<Plant, MoodBubble> _moodBubbles = new Dictionary<Plant, MoodBubble>();

    public void AllocateMoodBubble(Plant plant)
    {
        var bubble = Instantiate(_prefab, _container);
        _moodBubbles.Add(plant, bubble);
        bubble.SetIcon(plant.GetPersonality());
        UpdateBubble(bubble, plant);
    }

    public void Update()
    {
        foreach(var keypair in _moodBubbles)
        {
            UpdateBubble(keypair.Value, keypair.Key);
        }
    }

    public void UpdateBubble(MoodBubble bubble, Plant plant)
    {
        //Move the bubble here to correct position on the screen.
        var r = bubble.GetRectTransform();
        var pos = plant.gameObject.transform.position;
        var viewportPos = _camera.WorldToViewportPoint(pos);
        var isOnScreen = viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1 && viewportPos.z > 0;
        SetActive(bubble.gameObject, isOnScreen);
        if (!isOnScreen) return;
        var uiPos = new Vector2(((viewportPos.x * _canvasRect.sizeDelta.x) - (_canvasRect.sizeDelta.x * 0.5f)),
            ((viewportPos.y * _canvasRect.sizeDelta.y) - (_canvasRect.sizeDelta.y * 0.5f)));
        r.anchoredPosition = uiPos;
    }

    public void SetActive(GameObject obj, bool value)
    {
        if (obj.activeSelf == value) return;
        obj.SetActive(value);
    }

    public void ReleaseMoodBubble(Plant plant)
    {
        var bubble = _moodBubbles[plant];
        _moodBubbles.Remove(plant);
        Destroy(bubble.gameObject);
    }
}
