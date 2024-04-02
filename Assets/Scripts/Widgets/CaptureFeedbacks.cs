using UnityEngine;

public class CaptureFeedbacks : MonoBehaviour
{
    private ProgressBar _captureProgressBar;

    private void Awake()
    {
        _captureProgressBar = GetComponentInChildren<ProgressBar>();
    }

    public void SetCapturePercentage(float actual, float target)
    {
        _captureProgressBar.gameObject.SetActive(target > 0f);
        if (target > 0f)
            _captureProgressBar.SetProgress(Mathf.Clamp01(actual / target));
    }
}