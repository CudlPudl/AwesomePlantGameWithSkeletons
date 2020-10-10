using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootMenu : MonoBehaviour
{
    [SerializeField]
    private DummyLoader _loader;

    public void Start()
    {
        AudioPlayer.Instance.Reset();
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void StartGame()
    {
        var go = Instantiate(_loader);
        DontDestroyOnLoad(go.gameObject);
        //go.StartCoroutine(LoadGame(go));
        go.LoadScene();
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

    public void ExitGame()
    {
        Application.Quit();
    }
}
