using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Plant : MonoBehaviour
{
    public void Start()
    {
        _nutritionTimer = _nutritionCooldown;
    }

    public enum Personality
    {
        Cute = 0,
        Happy = 1,
        Sad = 2,
        Grumpy = 3,
        Neutral = 4
    }

    [System.Serializable]
    public class PlantEvent : UnityEvent<Plant> { }

    [HideInInspector]
    public PlantPot Pot;

    private float _growth = 0f;
    private bool _demandNutrition = false;
    private float _nutritionCooldown = 5f;
    private float _nutritionTimer = 5f;
    private float _demandTimer = 0f;
    private float _demandDelay = 0.1f;

    [SerializeField]
    private float _witherDuration = 20f;
    private float _witherTimer = 0f;
    private bool _isDying;

    [SerializeField]
    private Personality _personality = Personality.Cute;
    public Personality GetPersonality() => _personality;
    [SerializeField]
    private Transform _root;
    [SerializeField]
    private Transform _moodBubbleRoot;
    [SerializeField]
    private GrowthBlendshape _blendShape;
    [SerializeField]
    private PlantWitherVisualization _witherVisualization;
    [SerializeField]
    private float MaxGrowthDuration = 25f;
    [SerializeField]
    private float MaxSize = 5f;
    [SerializeField]
    private int _nutritionCost = 1;
    [SerializeField]
    private float _nutritionNeedChance = 0.2f;

    public int GetNutritionCost() => _nutritionCost;

    public PlantEvent OnPickEvent = new PlantEvent();
    public PlantEvent OnDemandNutrition = new PlantEvent();
    public PlantEvent OnNutritionGet = new PlantEvent();

    public float GetPlantSize() => Mathf.Clamp01(_growth / MaxGrowthDuration);
    public bool IsFullyGrown() => _growth >= MaxGrowthDuration;
    public Transform GetMoodBubblePosition() => _moodBubbleRoot;

    public void OnTapped()
    {
        if (!_demandNutrition) return;
        if (NutrientsManager.Instance.Purchase(NutrientsManager.Instance.GetNutrientTypeByPersonality(_personality), GetNutritionCost()))
        {
            OnNutritionGiven();
        }
    }

    public void SetPersonality(Personality type)
    {
        _personality = type;
    }

    private void Awake()
    {
        _nutritionTimer = _nutritionCooldown;
        //Avoid setting the correct growth state one frame too late.
        _blendShape.SetBlendValue(GetPlantSize());
    }

    private void Update()
    {
        if (!_demandNutrition)
        {
            _growth += Time.deltaTime;
            _blendShape.SetBlendValue(GetPlantSize());
            _nutritionTimer -= Time.deltaTime;
            if (_nutritionTimer <= 0)
            {
                TickNutritionNeed();
            }
        }
        if (_demandNutrition)
        {
            _witherTimer += Time.deltaTime;
            _witherVisualization.SetWitherLevel(_witherTimer / _witherDuration);
            if (_witherTimer >= _witherDuration * 0.5f && !_isDying)
            {
                _isDying = true;
                AudioPlayer.Instance.AddDeathFlag(this);
            }
            if (_witherTimer >= _witherDuration)
            {
                Die();
            }
        }
    }

    private void TickNutritionNeed()
    {
        _demandTimer += Time.deltaTime;
        if (_demandTimer >= _demandDelay)
        {
            _demandTimer = 0;
            if (Random.value < _nutritionNeedChance)
            {
                OnNeedNutrition();
            }
        }
    }

    private void OnNeedNutrition()
    {
        if (_demandNutrition) return;
        OnDemandNutrition.Invoke(this);
        _demandNutrition = true;
        _demandTimer = 0f;
        MoodBubbleManager.Instance.AllocateMoodBubble(this);
        _witherTimer = 0f;
    }

    private void OnNutritionGiven()
    {
        if (!_demandNutrition) return;
        OnNutritionGet.Invoke(this);
        _demandNutrition = false;
        _nutritionTimer = _nutritionCooldown;
        MoodBubbleManager.Instance.ReleaseMoodBubble(this);
        AudioPlayer.Instance.ClearFlag(this);
        _isDying = false;
        _witherTimer = 0f;
        _witherVisualization.SetWitherLevel(0f);
    }

    public void Reset()
    {
        MoodBubbleManager.Instance.ReleaseMoodBubble(this);
        AudioPlayer.Instance.ClearFlag(this);
    }

    private void Die()
    {
        Reset();
        if (Pot == null) return;
        Pot.RemovePlant();
    }
}
