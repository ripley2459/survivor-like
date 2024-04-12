using UnityEngine;

public class ExpBar : ProgressBar
{
    private PlayerLevel _playerLevel;

    protected override void Awake()
    {
        base.Awake();

        _playerLevel ??= GetComponentInParent<PlayerLevel>();
    }

    protected void Start()
    {
        _playerLevel.OnExpChanged += e => SetProgress(Mathf.Clamp01(e / (float)_playerLevel.ExpToNext));
    }
}