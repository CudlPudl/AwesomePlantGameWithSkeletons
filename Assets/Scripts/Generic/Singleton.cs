using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;
    public static T Instance => _instance;

    public void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(this);
            _instance = (T)FindObjectOfType(typeof(T));
            return;
        }
        Destroy(this);
    }
}
