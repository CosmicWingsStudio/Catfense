using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneEnemyFactory : MonoBehaviour
{
    [SerializeField] private Transform EnemyFolder;
    [SerializeField, Min(0.5f)] private float _enemySpawnDelay = 1.5f; 

    private SignalBus _signalBus;
    private PrefabsPathsToFoldersProvider _prefabsPathsToFoldersProvider;
    private List<Enemy> _enemyOnTheWave = new();
    private List<LevelWave> _wavesList;
    private List<LevelWaveEnemyInfo> _currentWaveData;

    private int _wavesAmount;
    private int _currentWave = 1;
    private float _enemySpawnDelayTimer = 0f;

    private bool IsReadyToProduceUnits = false;
    public bool AllEnemyOnTheWaveIsSpawned { get; private set; } = false;

    [Inject]
    private void Initialize(SignalBus signalBus, PrefabsPathsToFoldersProvider prefabsPathsProvider)
    {
        _signalBus = signalBus;
        _prefabsPathsToFoldersProvider = prefabsPathsProvider;

        _signalBus.Subscribe<WaveStartedSignal>(TurnOnProduceMode);
        _signalBus.Subscribe<WaveEndedSignal>(TurnOffProduceMode);
    }

    public void SetConfigData(List<LevelWave> wavesList, int wavesAmount)
    {
        _wavesList = wavesList;
        _wavesAmount = wavesAmount;

        _currentWaveData = _wavesList[0].EnemiesOnWaveList;
    }

    private void Update()
    {
        if (IsReadyToProduceUnits)
        {
            if(_enemySpawnDelayTimer >= _enemySpawnDelay)
            {
                if (_currentWaveData.Count != 0)
                {
                    for (int i = 0; i < _currentWaveData.Count; i++)
                    {
                        if (_currentWaveData[i].Amount > 0)
                        {
                            try
                            {
                                _enemyOnTheWave.Add(Produce(BuildPathToEnemyPrefabsFolder(_currentWaveData[i].PrefabName)));
                                _currentWaveData[i].Amount -= 1;
                                _enemySpawnDelayTimer = 0f;
                                break;
                            }
                            catch (System.Exception)
                            {
                                Debug.LogError("Failed to spawn Enemy Prefab");
                            }

                        }
                        else if(i+1 == _currentWaveData.Count) //если ласт заказ на противника амаунт = 0, то выходит что больше некого продюсить
                        {
                            AllEnemyOnTheWaveIsSpawned = true;
                            IsReadyToProduceUnits = false;
                            Debug.Log("AllEnemyOnTheWaveIsSpawned = " + AllEnemyOnTheWaveIsSpawned);
                        }

                    }
                }
                else
                    Debug.LogError("No Enemy on the wave");
                //взять текущую волну мб в отделньое поле и из нее вычитать, а также какой-то отдельное поле-итератор для волн
            }
            else
                _enemySpawnDelayTimer += Time.deltaTime;
            
        }
    }

    private Enemy Produce(string PathToPrefab)
    {
        Enemy enemy = Instantiate(Resources.Load<Enemy>(PathToPrefab), EnemyFolder);
        return enemy;
    }

    public bool CheckCurrentWaveEnemyListIsEmptylOrNot()
    {
        if (AllEnemyOnTheWaveIsSpawned)
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
        else
            return false;

    }

    private string BuildPathToEnemyPrefabsFolder(string NameOfPrefab)
    {
        return _prefabsPathsToFoldersProvider.EnemyUnitsPrefabsPath + NameOfPrefab;
    }

    private void TurnOffProduceMode()
    {
        IsReadyToProduceUnits = false;
        _enemyOnTheWave.Clear();
        if (_currentWave + 1 <= _wavesAmount)
            _currentWave++;
    }

    private void TurnOnProduceMode()
    {
        IsReadyToProduceUnits = true;
        AllEnemyOnTheWaveIsSpawned = false;
        _enemySpawnDelayTimer = _enemySpawnDelay;
        _currentWaveData = _wavesList[_currentWave - 1].EnemiesOnWaveList;
    }
}
