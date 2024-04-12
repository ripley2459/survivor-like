using UnityEngine;
using UnityEngine.UI;

public abstract class ProgressBar : MonoBehaviour
{
    private Slider _slider;

    protected virtual void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public virtual void SetProgress(float value)
    {
        _slider.value = value;
    }
}