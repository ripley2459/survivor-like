using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    [SerializeField] private float _timeBetweenNotifications = 0.5f;
    private readonly Queue<Notification> _notificationsQueue = new Queue<Notification>();
    private TextMeshProUGUI _text;
    private Coroutine _showNotifications;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _text.color = Color.clear;
    }

    public void PushNotification(Notification notification)
    {
        _notificationsQueue.Enqueue(notification);

        if (ReferenceEquals(_showNotifications, null))
            _showNotifications = StartCoroutine(ShowNotifications());
    }

    private IEnumerator ShowNotifications()
    {
        while (_notificationsQueue.Count > 0)
        {
            yield return StartCoroutine(ShowNotification(_notificationsQueue.Dequeue()));
            yield return new WaitForSeconds(_timeBetweenNotifications);
        }

        _showNotifications = null;
    }

    private IEnumerator ShowNotification(Notification notification)
    {
        _text.text = notification.Text;
        Color clearColor = new Color(notification.Color.r, notification.Color.g, notification.Color.b, 0f);
        float duration = 0f;

        while (duration < notification.TransitionDuration)
        {
            _text.color = Color.Lerp(clearColor, notification.Color, duration / notification.TransitionDuration);
            duration += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(notification.DisplayDuration);

        while (duration > 0f)
        {
            _text.color = Color.Lerp(clearColor, notification.Color, duration / notification.TransitionDuration);
            duration -= Time.deltaTime;
            yield return null;
        }
    }

    [Serializable]
    public struct Notification
    {
        [SerializeField] private string _text;
        [SerializeField] private Color _color;
        [SerializeField] private float _displayDuration;
        [SerializeField] private float _transitionDuration;

        public string Text => _text;

        public Color Color => _color;

        public float DisplayDuration => _displayDuration;

        public float TransitionDuration => _transitionDuration;
    }
}