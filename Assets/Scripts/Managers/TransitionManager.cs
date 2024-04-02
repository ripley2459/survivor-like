using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : APersistentManager<TransitionManager>
{
    [SerializeField] private LoadingScreen _loadingScreen;
    private GameMaster.EGameState _futureState;

    public Action<string> OnSceneChanged;

    protected override void Awake()
    {
        base.Awake();

        // _loadingScreen = GetComponentInChildren<LoadingScreen>(); // Can't get loading screen automatically because manually disabled!
    }

    private void Start()
    {
        HideLoadingScreen();
    }

    public void ShowLoadingScreen()
    {
        _loadingScreen.gameObject.SetActive(true);
    }

    public void HideLoadingScreen()
    {
        _loadingScreen.gameObject.SetActive(false);
    }

    private void SetProgress(float progress)
    {
        _loadingScreen.SetProgress(Mathf.Clamp01(progress));
    }

    /// <summary>
    /// Loads the scene with the provided name asynchronously.
    /// </summary>
    /// <param name="scene">The name of the scene to load.</param>
    /// <param name="showLoadingScreen">If set to true the loading screen will shown during the process.</param>
    /// <returns>The asynchronous task.</returns>
    private IEnumerator LoadScene(string scene, bool showLoadingScreen = true)
    {
        GameMaster.Instance.State = GameMaster.EGameState.Loading;
        if (showLoadingScreen)
            ShowLoadingScreen();
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            SetProgress(Mathf.Clamp01(operation.progress / 0.9f));
            yield return null;
        }

        yield return new WaitForEndOfFrame();
        OnSceneChanged?.Invoke(scene);
        GameMaster.Instance.State = _futureState;
        HideLoadingScreen();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scene">The scene to open.</param>
    /// <param name="state">The state to set when the scene is loaded and opened.</param>
    public void Navigate([CanBeNull] string scene, GameMaster.EGameState state)
    {
        if (!string.IsNullOrEmpty(scene) && scene != SceneManager.GetActiveScene().name)
        {
            _futureState = state;
            StartCoroutine(LoadScene(scene));
        }
        else GameMaster.Instance.State = state;
    }
}