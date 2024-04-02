using System;
using TMPro;
using UnityEngine;

public class TimerFeedbacks : MonoBehaviour
{
    [SerializeField] private string _format = "00:00:00";
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void SetTime(float time)
    {
        TimeSpan timeSpan = System.TimeSpan.FromMilliseconds(time);
        _text.text = timeSpan.Seconds < 10 ? "0" + timeSpan.Seconds : "" + timeSpan.Seconds;
        _text.text += ":" + (timeSpan.Milliseconds < 10 ? "0" + timeSpan.Milliseconds : "" + timeSpan.Milliseconds);
    }
}