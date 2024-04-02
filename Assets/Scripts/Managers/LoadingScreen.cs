using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private ProgressBar _progressBar;

    private void Awake()
    {
        _progressBar = GetComponentInChildren<ProgressBar>();
    }

    public void SetProgress(float value)
    {
        _progressBar.SetProgress(value);
    }
}