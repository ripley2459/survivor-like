using System;
using UnityEngine;

public class PlayerCapture : MonoBehaviour
{
    private CapturablePoint _capturablePoint;
    private CaptureFeedbacks _captureFeedbacks;
    public PlayerController PlayerController { get; private set; }

    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        _captureFeedbacks = GetComponentInChildren<CaptureFeedbacks>();
    }

    private void Start()
    {
        IsCapturingPoint(null);
    }

    public void IsCapturingPoint(CapturablePoint capturablePoint)
    {
        _capturablePoint = capturablePoint;
        _captureFeedbacks.gameObject.SetActive(!ReferenceEquals(_capturablePoint, null));
    }

    private void Update()
    {
        if (ReferenceEquals(_capturablePoint, null))
            return;

        _captureFeedbacks.SetCapturePercentage(_capturablePoint.ActualCaptureDuration, _capturablePoint.CaptureDuration);
    }

    public enum CaptureState
    {
        /// <summary>
        /// The point is ready to be captured.
        /// </summary>
        Ready,

        /// <summary>
        /// The point has been captured.
        /// </summary>
        Captured,

        /// <summary>
        /// The point has no no time left but there is at least one player.
        /// </summary>
        Prolongations,

        /// <summary>
        /// The point is unusable.
        /// </summary>
        Deactivated
    }
}