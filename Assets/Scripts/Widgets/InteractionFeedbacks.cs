using System;
using TMPro;
using UnityEngine;

public class InteractionFeedbacks : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _interactionText;
    private ProgressBar _interactionProgressBar;
    private PlayerInteraction _playerInteraction;

    private void Awake()
    {
        _interactionProgressBar = GetComponentInChildren<ProgressBar>();
    }

    public void SetInteractionPercentage(float actual, float target)
    {
        _interactionProgressBar.gameObject.SetActive(target > 0f);
        if (target > 0f)
            _interactionProgressBar.SetProgress(Mathf.Clamp01(actual / target));
    }

    public void SetInteractionText(String text)
    {
        _interactionText.text = text;
    }
}