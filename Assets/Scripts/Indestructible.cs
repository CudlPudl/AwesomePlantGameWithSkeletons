using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indestructible : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
