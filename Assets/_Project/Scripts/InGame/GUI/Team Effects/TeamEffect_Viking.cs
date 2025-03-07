using System.Collections.Generic;
using UnityEngine;

public class TeamEffect_Viking : TeamEffect_Unit
{
    [SerializeField] private EnvironmentHandler _env;
    private EnvironmentContainerHandler _envContainerHandler;
    private bool IsInititalised = false;

    private void Initialize()
    {
        _envContainerHandler = _env.GetEnvironmentContainer();
        IsInititalised = true;
    }

    protected override void ApplyEffect()
    {
        if (!IsInititalised)
            Initialize();

        switch (_currentNumber)
        {
            case 2:
                ApplyEffectOnDefinedUnits(FindUnits(_envContainerHandler.GetAllPlaceableSlots()), _step1);
                break;
            case 3:
                ApplyEffectOnDefinedUnits(FindUnits(_envContainerHandler.GetAllPlaceableSlots()), _step2);
                break;
            case 4:
                ApplyEffectOnDefinedUnits(FindUnits(_envContainerHandler.GetAllPlaceableSlots()), _step3);
                break;
            default:
                ApplyEffectOnDefinedUnits(FindUnits(_envContainerHandler.GetAllPlaceableSlots()), 0);
                break;
        }
    }

    private List<PlaceableUnit> FindUnits(List<PlaceSlot> slots)
    {
        List<PlaceableUnit> units = new List<PlaceableUnit>();

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].Item != null)
            {
                if (slots[i].Item.TryGetComponent(out PlaceableUnit unit))
                {
                    units.Add(unit);
                }
            }
        }

        return units;
    }

    private void ApplyEffectOnDefinedUnits(List<PlaceableUnit> units, float step)
    {
        for (int i = 0; i < units.Count; i++)
        {
            bool value = true;
            if(step == 0)
                value = false;

            units[i].Health.SetReviveAbility(step, value);
        }
    }
}
