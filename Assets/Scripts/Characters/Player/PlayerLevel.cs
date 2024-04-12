using System;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private int _actualExp = 0;
    [SerializeField] private int _expToNext = 10;
    [SerializeField] private int _increasedExp = 5;
    private int _actualLevel = 1;

    public Action<int> OnExpChanged;
    public Action<int> OnLevelChanged;

    public int ActualExp
    {
        get => _actualExp;
        set
        {
            _actualExp = value;
            if (_actualExp < 0)
                _actualExp = 0;
            OnExpChanged?.Invoke(_actualExp);
        }
    }

    public int ExpToNext
    {
        get => _expToNext;
        set
        {
            int previous = _expToNext;
            _expToNext = value;
            if (_expToNext <= 0)
                _expToNext = 1;
            if (_expToNext != previous)
                GainExp(0);
        }
    }

    public int ActualLevel
    {
        get => _actualLevel;
        set
        {
            _actualLevel = value;
            if (_actualLevel < 1)
                _actualLevel = 1;
            OnLevelChanged?.Invoke(_actualLevel);
        }
    }

    public void GainExp(int exp)
    {
        if (exp <= 0)
            return;

        ActualExp += exp;
        while (ActualExp >= ExpToNext)
            GainLevel();
    }

    private void GainLevel()
    {
        ActualExp = 0;
        ActualLevel++;
        ExpToNext = ExpToNext + (ActualLevel - 1) * _increasedExp;
    }
}