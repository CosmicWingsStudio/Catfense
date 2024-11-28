using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyContainerHandler : IDisposable, ITickable
{
    private SceneEnemyFactory _enemyFactory;

    public EnemyContainerHandler(SceneEnemyFactory enemyFactory, SignalBus _signalBus)
    {
        _enemyFactory = enemyFactory;
        
    }

    public List<Enemy> _enemyOnTheWave = new();

    public bool CheckCurrentWaveEnemyListIsEmptylOrNot()
    {
        if (_enemyOnTheWave.Count > 0)
        {
            for (int i = 0; i < _enemyOnTheWave.Count; i++)
            {
                if (_enemyOnTheWave[i] != null)
                    return false;
            }
            return true;
        }
        else
        {
            return true;
        }
    }
}
