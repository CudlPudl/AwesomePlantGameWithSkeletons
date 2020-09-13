﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootMenu : MonoBehaviour
{
    public void GoToCredits()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void StartGame()
    {
        var go = new GameObject("GameLoader");
        var dummyLoader = go.AddComponent<DummyLoader>();
        DontDestroyOnLoad(go);
        dummyLoader.StartCoroutine(LoadGame(dummyLoader));
    }

    public IEnumerator LoadGame(DummyLoader loader)
    {
        var envScene = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        envScene.allowSceneActivation = false;
        var gameScene = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        gameScene.allowSceneActivation = false;
        while (gameScene.progress < 0.9f || envScene.progress < 0.9f)
        {
            yield return null;
        }
        gameScene.allowSceneActivation = true;
        envScene.allowSceneActivation = true;

        while (!gameScene.isDone || !envScene.isDone) yield return null;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        Destroy(loader.gameObject);
    }
}