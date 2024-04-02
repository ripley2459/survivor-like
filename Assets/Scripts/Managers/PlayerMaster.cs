using System;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class PlayerMaster : APersistentManager<PlayerMaster>
{
    #region StateManagement

    private EPlaterState _state;

    public Action<EPlaterState> OnStateChanged;

    public EPlaterState State
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

    public enum EPlaterState
    {
        Unknown,
        Loading,
        Menu,
        Game
    }

    #endregion StateManagement

    protected override void Awake()
    {
        base.Awake();

        OnStateChanged += ChangeMouseBehaviour;
    }

    private void ChangeMouseBehaviour(EPlaterState newState)
    {
    }
}