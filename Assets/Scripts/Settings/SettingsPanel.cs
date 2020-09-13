using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;

    public void TogglePanel()
    {
        obj.SetActive(!obj.activeSelf);
    }
}
