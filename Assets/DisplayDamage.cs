using TMPro;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    private Canvas _canvas;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetDamage(int damage)
    {
        _text.text = damage.ToString();
        Invoke(nameof(KillAfter), 3f);
    }
    
    private void KillAfter()
    {
        gameObject.SetActive(false);
    }
}