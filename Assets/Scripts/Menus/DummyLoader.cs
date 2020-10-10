using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DummyLoader : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        Destroy(gameObject);
    }
}
