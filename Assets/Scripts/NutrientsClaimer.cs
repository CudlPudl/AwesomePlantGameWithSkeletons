using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientsClaimer : MonoBehaviour
{
    [SerializeField]
    private Nutrient.NutrientType _type;
    public void ClaimNutrients()
    {
        NutrientsManager.Instance.ClaimStorage(_type);
    }
}
