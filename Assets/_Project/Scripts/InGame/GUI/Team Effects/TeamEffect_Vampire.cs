using UnityEngine;

public class TeamEffect_Vampire : TeamEffect_Unit
{
    [SerializeField] private EnvironmentHandler _envHandler;

    private EnvironmentContainerHandler _envContainer;
    private bool IsInititalised = false;

    private void Initialize()
    {
        _envContainer = _envHandler.GetEnvironmentContainer();
        IsInititalised = true;
    }

    protected override void ApplyEffect()
    {
        if(!IsInititalised)
            Initialize();

        switch (_currentNumber)
        {
            case 2:
                _envContainer.HealingBonus = _step1;
                break;
            case 3:
                _envContainer.HealingBonus = _step2;
                break;
            case 4:
                _envContainer.HealingBonus = _step3;
                break;
            default:
                _envContainer.HealingBonus = 0;
                break;
        }
    }
}
