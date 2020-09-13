using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, Range(0.01f, 2f)]
    private float globalTimescale = 1f;

    [Header("Plant")]
    [SerializeField, Tooltip("Minimum time delay between plant requesting more nutrients")]
    private float plantMinNutritionCooldown = 5f;
    public float GetPlantMinNutritionCooldown() => plantMinNutritionCooldown * globalTimescale;
    [SerializeField, Tooltip("Once minimum delay between demanding nutrients, delay between rolling dice for next need")]
    private float plantNutritionDemandDelay = 0.1f;
    public float GetPlantNutritionDemandDelay() => plantNutritionDemandDelay * globalTimescale;
    [SerializeField, Tooltip("Each time we roll dice, the chance for needing nutrients. 0 is 0% and 1 is 100%")]
    private float plantNutritionNeedChance = 0.2f;
    public float GetPlantNutritionNeedChance() => plantNutritionNeedChance;
    [SerializeField, Tooltip("How long does it take for the plant to be fully grown up?")]
    private float plantMaxGrowthDuration = 25f;
    public float GetPlantMaxGrowthDuration() => plantMaxGrowthDuration * globalTimescale;
    [SerializeField, Tooltip("Time it takes for the plants to start withering")]
    private float plantWitherDuration = 20f;
    public float GetPlantWitherDuration() => plantWitherDuration * globalTimescale;

    [Header("Activities")]
    [SerializeField, Tooltip("How long you have to hold till you've generated nutrients. Counts full hold duration only")]
    private float holdTimeForNutrient = 1f;
    public float GetActivityHoldTimeForNutrient() => holdTimeForNutrient * globalTimescale;

    [Header("NutrientsManager")]
    [SerializeField, Tooltip("Gives light nutrient every set amount of time")]
    private float nutrientsLightGenerationInterval = 4f;
    public float GetNutrientsLightGenerationInterval() => nutrientsLightGenerationInterval * globalTimescale;
}
