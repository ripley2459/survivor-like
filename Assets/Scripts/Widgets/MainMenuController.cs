using System;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _panelBase;
    [SerializeField] private GameObject _panelPlay;
    [SerializeField] private GameObject _panelSettings;
    [SerializeField] private GameObject _panelQuit;

    #region StateManagement

    private EMainMenuState _state;

    public Action<EMainMenuState> OnStateChanged;

    public EMainMenuState State
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

    public enum EMainMenuState
    {
        Unknown,
        Main,
        Play,
        Settings,
        Quit
    }

    #endregion StateManagement

    private void Awake()
    {
        OnStateChanged += ChangeActivePanel;
    }

    private void Start()
    {
        OpenPanelMain();
    }

    public void OpenDesolation()
    {
        TransitionManager.Instance.Navigate("Desolation", GameMaster.EGameState.Unknown);
    }

    public void OpenPanelMain()
    {
        print(State);
        State = EMainMenuState.Main;
        print(State);
    }

    public void OpenPanelPlay()
    {
        State = EMainMenuState.Play;
    }

    public void OpenPanelSettings()
    {
        State = EMainMenuState.Settings;
    }

    public void OpenPanelQuit()
    {
        State = EMainMenuState.Quit;
    }

    private void ChangeActivePanel(EMainMenuState newState)
    {
        _state = newState;

        _panelBase.SetActive(newState == EMainMenuState.Main);
        _panelPlay.SetActive(newState == EMainMenuState.Play);
        _panelSettings.SetActive(newState == EMainMenuState.Settings);
        _panelQuit.SetActive(newState == EMainMenuState.Quit);
    }
}