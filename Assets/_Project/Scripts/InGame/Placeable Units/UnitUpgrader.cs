using UnityEngine;

[RequireComponent(typeof(UnitDataDisplayer))]
public class UnitUpgrader : MonoBehaviour
{
    private int _currentUpgradeLevel = 0;
    private float _scaleMultiplier = 0f;
    private UnitDataDisplayer _displayer;
    private PlaceableUnit _placeableUnit;

    public readonly int MaxUpgradeLevel = 3;

    private void Start()
    {
        _displayer = GetComponent<UnitDataDisplayer>();
        _placeableUnit = GetComponent<PlaceableUnit>();
    }

    public bool CheckIfPossibleToUpgrade()
    {
        if (_currentUpgradeLevel + 1 <= MaxUpgradeLevel)
            return true;
        else
            return false;
    }

    public void UpgradeUnit()
    {
        _currentUpgradeLevel++;
        _scaleMultiplier += 0.25f;
        _displayer.ShowUnitUpgrade();
        _placeableUnit.unitAttackHandler.UpgradeStats(_scaleMultiplier);
        _placeableUnit.Health.UpgradeStats(_scaleMultiplier);

        FxSpawner.Instance.SpawnUpgradeEffect(transform.position);
        SoundMakerGUI.Instance.PlaySoundInSubAudioSource(SoundMakerGUI.Instance.SoundUpgradeUnit);
    }

    public int GetCurrentUpgradeLevel() => _currentUpgradeLevel;

}
