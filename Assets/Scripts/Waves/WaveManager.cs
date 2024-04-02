using System;
using UnityEngine;

/// <summary>
/// Les WaveManager permet de créer des vagues d'ennemis que les joueurs vont devoir tous tuer pour progresser.
/// Chaque vague est divisable en round, quand tout les rounds d'un vagues sont terminés on passe à la vague suivante et ainsi de suite jusqu'a ce qu'il n'y ai plus aucune vague.
/// Un round est composé de Spawners qui invoquent tout leur ennemis aux point d'apparition attribués.
/// Mais également de CapturablePoints que les joueurs doivent capturer.
/// Si au moins un capturable point n'est pas encore capturé, les spawner réinvoquent ses ennemis quand ils sont tous mort.
/// Le round est terminé quand tous les ennemis sont mort et que tout les points sont capturé.
/// Si plus aucun joueur ne sont en vie, ou si les tout les points ne sont pas capturé avant la fin du temps imparti, c'est la défaite.
/// </summary>
public class WaveManager : AManager<WaveManager>
{
    [SerializeField] private Wave[] _waves;
    
    private int _activeWave;
    private int _activeRound;
    
    public Wave GetWave()
    {
        return _waves[_activeWave];
    }

    public Round GetRound()
    {
        return _waves[_activeWave].Rounds[_activeRound];
    }

    private void Start()
    {
        // StartRound(GetRound());
    }
    
    public void StartRound(Round round)
    {
        foreach (var spawner in round.SpawnPoints)
        {
            foreach (var poolable in spawner.Poolable)
            {
                var poolableObject = poolable;

                var spawned = PoolingManager.Instance.Spawn(ref poolableObject, R.GetRandom(spawner.Positions).position, Quaternion.identity);
            }
        }
    }

    #region Logic

    #endregion Logic

    #region Data

    [Serializable]
    public struct Wave
    {
        [SerializeField] private Round[] _rounds;

        public Round[] Rounds => _rounds;
    }

    [Serializable]
    public struct Round
    {
        [SerializeField] private Spawner[] _spawnPoints;
        [SerializeField] private Capture _capture;

        public Spawner[] SpawnPoints => _spawnPoints;

        public Capture Capture => _capture;
    }

    [Serializable]
    public struct Spawner
    {
        [SerializeField] private Transform[] _positions;
        [SerializeField] private PoolingManager.PoolableObject[] _poolable;

        public Transform[] Positions => _positions;

        public PoolingManager.PoolableObject[] Poolable => _poolable;
    }

    [Serializable]
    public struct Capture
    {
        [SerializeField] private CapturablePoint[] _points;
        [SerializeField] private float _timeLeft;
        [SerializeField] private float _captureDuration;

        public CapturablePoint[] Points => _points;

        public float TimeLeft => _timeLeft;

        public float CaptureDuration => _captureDuration;
    }

    #endregion Data
}