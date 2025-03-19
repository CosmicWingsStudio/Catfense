using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneEnemyFactory : MonoBehaviour
{
    [SerializeField, Min(0.5f)] private float _enemySpawnDelay = 2.25f; 

    private Transform _enemySpawnPoint;
    private SignalBus _signalBus;
    private PrefabsPathsToFoldersProvider _prefabsPathsToFoldersProvider;
    private RewardSpawner _rewardSpawner;
    private List<EnemyUnit> _enemyOnTheWave = new();
    private List<LevelWave> _wavesList;
    private List<LevelWaveEnemyInfo> _currentWaveData;

    public int WavesAmount { private set; get; }

    private float _enemySpawnDelayTimer = 0f;
    private float _difficultyLevel = 1f;
    private float _difficultyScale = 0.1f;
    private float _movementModifire = 0f;
    private float _currentDifficultyLevel = 0f;
    private float _enemySpawnDelayRandomised;

    private bool IsReadyToProduceUnits = false;
    private bool IsPaused = false;
    public bool AllEnemyOnTheWaveIsSpawned { get; private set; } = false;
    public bool IsLastWave { get; private set; } = false;
    public bool IsSpawnDisabledByDevTools { get; set; } = false;

    [HideInInspector] public int CurrentWave = 1;

    [Inject]
    private void Initialize(SignalBus signalBus, PrefabsPathsToFoldersProvider prefabsPathsProvider, RewardSpawner rewardSpawner)
    {
        _signalBus = signalBus;
        _prefabsPathsToFoldersProvider = prefabsPathsProvider;
        _rewardSpawner = rewardSpawner;

        _signalBus.Subscribe<WaveStartedSignal>(HandleWaveStartsSignal);
        _signalBus.Subscribe<WaveEndedSignal>(HandleWaveEndsSignal);
        _signalBus.Subscribe<PausedSignal>(() => IsPaused = true);
        _signalBus.Subscribe<UnpausedSignal>(() => IsPaused = false);
    }

    public void SetConfigData(List<LevelWave> wavesList, int wavesAmount, Transform enemySpawnPoint,
        float difficultyLevel, float difficultyScale, float movementModifire)
    {
        _wavesList = new();
        _difficultyLevel = difficultyLevel;
        _movementModifire = movementModifire;
        _currentDifficultyLevel = difficultyLevel;
        _difficultyScale = difficultyScale;

        for (int i = 0; i < wavesList.Count; i++)
        {
            List<LevelWaveEnemyInfo> lwei = new();

            for (int j = 0; j < wavesList[i].EnemiesOnWaveList.Count; j++)
            {
                lwei.Add(new LevelWaveEnemyInfo(wavesList[i].EnemiesOnWaveList[j].PrefabName, wavesList[i].EnemiesOnWaveList[j].Amount));
            }

            _wavesList.Add(new LevelWave(lwei));
        }

        WavesAmount = wavesAmount;
        _enemySpawnPoint = enemySpawnPoint;

        _currentWaveData = _wavesList[0].EnemiesOnWaveList;
    }

    private void Update()
    {
        if (IsPaused || IsSpawnDisabledByDevTools)
            return;
        if (IsReadyToProduceUnits)
        {
            if (_enemySpawnDelayTimer >= _enemySpawnDelayRandomised)
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
                                RandomizeSpawnDelay();
                                _currentWaveData[i].Amount -= 1;
                                _enemySpawnDelayTimer = 0f;
                                break;
                            }
                            catch (System.Exception)
                            {
                                Debug.LogError("Failed to spawn Enemy Prefab");
                            }

                        }
                        else if (i + 1 == _currentWaveData.Count)
                        {
                            AllEnemyOnTheWaveIsSpawned = true;
                            IsReadyToProduceUnits = false;
                        }

                    }
                }
                else
                    Debug.LogError("No Enemy on the wave");
            }
            else
                _enemySpawnDelayTimer += Time.deltaTime;

        }


    }

    private EnemyUnit Produce(string PathToPrefab)
    {
        EnemyUnit enemy = Instantiate(Resources.Load<EnemyUnit>(PathToPrefab), _enemySpawnPoint);
        enemy.Initialize(_currentDifficultyLevel, _rewardSpawner, _movementModifire);
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

    private void HandleWaveEndsSignal()
    {
        IsReadyToProduceUnits = false;
        AllEnemyOnTheWaveIsSpawned = false;
        _enemyOnTheWave.Clear();
        if (CurrentWave + 1 <= WavesAmount) 
            CurrentWave++;    
    }

    private void HandleWaveStartsSignal()
    {
        IsReadyToProduceUnits = true;
        _enemySpawnDelayTimer = _enemySpawnDelay;

        if(CurrentWave != 1)
        {
            _currentDifficultyLevel = _difficultyLevel + (_difficultyLevel * _difficultyScale);
            _movementModifire += 0.0025f;
        }

        _currentWaveData = _wavesList[CurrentWave - 1].EnemiesOnWaveList;
        if (CurrentWave == WavesAmount)
            IsLastWave = true;
    }

    private void RandomizeSpawnDelay()
    {
        _enemySpawnDelayRandomised = Random.Range(_enemySpawnDelay, _enemySpawnDelay + 0.25f);
    }
}
