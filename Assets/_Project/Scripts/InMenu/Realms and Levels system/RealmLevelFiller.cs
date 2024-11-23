using TMPro;
using UnityEngine;

public class RealmLevelFiller : MonoBehaviour
{
    [Header("Set config")]
    [SerializeField, Tooltip("Take cfg from _Project/Levels Configs")] 
    private LevelConfig _levelConfig;

    [Header("Health Settings")]
    [SerializeField] private TextMeshProUGUI _wavesAmount;
    [SerializeField] private string _additionalTextToWavesAmount = "Waves";

    private void Start()
    {
        FillUIElementsWithInformation();
    }

    private void FillUIElementsWithInformation()
    {
        _wavesAmount.text = _levelConfig.WavesAmount.ToString() + " " +_additionalTextToWavesAmount;
    }

    public LevelConfig GetLevelConfig() => _levelConfig;
  
}
