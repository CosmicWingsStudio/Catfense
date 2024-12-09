using UnityEngine;

[RequireComponent(typeof(UnitDataDisplayer))]
public class UnitUpgrader : MonoBehaviour
{
    private int _currentUpgradeLevel = 0;
    private UnitDataDisplayer _displayer;

    public readonly int MaxUpgradeLevel = 3;

    private void Start()
    {
        _displayer = GetComponent<UnitDataDisplayer>();
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
        _displayer.ShowUnitUpgrade();
    }

    public int GetCurrentUpgradeLevel() => _currentUpgradeLevel;

}
