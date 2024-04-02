using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : AManager<PlayersManager>
{
    [SerializeField] private List<PlayerController> _playerControllers = new List<PlayerController>();

    public List<PlayerController> PlayerControllers => _playerControllers;
}