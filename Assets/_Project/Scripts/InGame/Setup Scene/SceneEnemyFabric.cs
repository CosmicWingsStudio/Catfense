using UnityEngine;
using Zenject;

public class SceneEnemyFabric : MonoBehaviour 
{
    [SerializeField] private Transform EnemyFolder;
    private SignalBus _signalBus;

    private bool IsReadyToProduceUnits = false;

    [Inject]
    private void Initialize(SignalBus signalBus)
    {
        _signalBus = signalBus;

        _signalBus.Subscribe<WaveStartedSignal>(TurnOnProduceMode);
        _signalBus.Subscribe<WaveEndedSignal>(TurnOffProduceMode);
    }

    private void Update()
    {
        if (IsReadyToProduceUnits)
        {

        }
    }

    private void Produce(string PathToPrefab)
    {
        Instantiate(Resources.Load(PathToPrefab), EnemyFolder);
    }



    private void TurnOffProduceMode()
    {
        IsReadyToProduceUnits = false;
    }

    private void TurnOnProduceMode()
    {
        IsReadyToProduceUnits = true;
    }
}
