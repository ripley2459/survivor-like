using System;
using UnityEngine;

[DefaultExecutionOrder(-3)]
public class GameMaster : APersistentManager<GameMaster>
{
    #region StateManagement

    private EGameState _state;

    public Action<EGameState> OnStateChanged;

    public EGameState State
    {
        get => _state;
        set
        {
            if (_state != value)
            {
                _state = value;
                OnStateChanged?.Invoke(_state);
            }
        }
    }

    public enum EGameState
    {
        Unknown,
        Init,
        Loading,
        MainMenu,
        Desolation
    }

    #endregion StateManagement
}