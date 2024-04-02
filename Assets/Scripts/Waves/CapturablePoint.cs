using System;
using System.Collections.Generic;
using UnityEngine;

public class CapturablePoint : MonoBehaviour
{
    [SerializeField] private float _captureDuration = 5f;
    [SerializeField] private float _timeLeft = 10f;
    [SerializeField] private Notifications.Notification _notificationCaptured;
    [SerializeField] private Notifications.Notification _notificationFailed;
    [SerializeField] private Notifications.Notification _notificationProlongation;
    private readonly List<PlayerCapture> _playerCaptures = new List<PlayerCapture>();
    public float CaptureDuration => _captureDuration;
    public float ActualCaptureDuration { get; private set; } = 0f;
    private TimerFeedbacks _timerFeedbacks;
    public PlayerCapture.CaptureState CaptureState { get; private set; } = PlayerCapture.CaptureState.Ready;

    private void Awake()
    {
        _timerFeedbacks = GetComponentInChildren<TimerFeedbacks>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerCapture>(out PlayerCapture playerCapture) || !CanBeCaptured())
            return;

        _playerCaptures.Add(playerCapture);
        playerCapture.IsCapturingPoint(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<PlayerCapture>(out PlayerCapture playerCapture))
            return;

        _playerCaptures.Remove(playerCapture);
        playerCapture.IsCapturingPoint(null);
    }

    private void Update()
    {
        if (!CanBeCaptured())
        {
            if (!ReferenceEquals(_timerFeedbacks, null))
                _timerFeedbacks.gameObject.SetActive(false);
            return;
        }

        _timeLeft -= Time.deltaTime;

        if (!ReferenceEquals(_timerFeedbacks, null))
        {
            _timerFeedbacks.gameObject.SetActive(true);
            _timerFeedbacks.SetTime(_timeLeft);
        }

        if (_timeLeft <= 0)
        {
            if (_playerCaptures.Count > 0)
            {
                if (CaptureState == PlayerCapture.CaptureState.Ready)
                {
                    CaptureState = PlayerCapture.CaptureState.Prolongations;

                    foreach (PlayerController playerController in PlayersManager.Instance.PlayerControllers)
                        playerController.PushNotification(_notificationProlongation);
                }
            }
            else
            {
                CaptureState = PlayerCapture.CaptureState.Deactivated;

                foreach (PlayerCapture playerCapture in _playerCaptures)
                    playerCapture.IsCapturingPoint(null);

                foreach (PlayerController playerController in PlayersManager.Instance.PlayerControllers)
                    playerController.PushNotification(_notificationFailed);
            }
        }

        if (_playerCaptures.Count > 0)
            ActualCaptureDuration += Time.deltaTime;

        if (ActualCaptureDuration >= _captureDuration)
        {
            CaptureState = PlayerCapture.CaptureState.Captured;

            foreach (PlayerCapture playerCapture in _playerCaptures)
                playerCapture.IsCapturingPoint(null);

            foreach (PlayerController playerController in PlayersManager.Instance.PlayerControllers)
                playerController.PushNotification(_notificationCaptured);
        }
    }

    public bool CanBeCaptured()
    {
        return CaptureState == PlayerCapture.CaptureState.Ready || CaptureState == PlayerCapture.CaptureState.Prolongations;
    }
}