using System.Collections.Generic;
using UnityEngine;

public class TeamEffects : MonoBehaviour
{
    [SerializeField] private List<TeamEffect> TeamEffectsList = new List<TeamEffect>();

    public void UpdateEffects(List<TeamEffectsData> effects)
    {
        TurnOffEffectsBeforeUpdate();
        for (int i = 0; i < effects.Count; i++)
        {
            switch (effects[i].EffectType)
            {
                case TeamEffectsType.Pirate:
                    TeamEffectsList[0].EnableEffect();
                    TeamEffectsList[0].UpdateEffect(effects[i].Amount);
                    break;

                case TeamEffectsType.Vampire:
                    TeamEffectsList[1].EnableEffect();
                    TeamEffectsList[1].UpdateEffect(effects[i].Amount);
                    break;

                case TeamEffectsType.Snow:
                    TeamEffectsList[2].EnableEffect();
                    TeamEffectsList[2].UpdateEffect(effects[i].Amount);
                    break;

                case TeamEffectsType.Alchemist:
                    TeamEffectsList[3].EnableEffect();
                    TeamEffectsList[3].UpdateEffect(effects[i].Amount);
                    break;

                case TeamEffectsType.Viking:
                    TeamEffectsList[4].EnableEffect();
                    TeamEffectsList[4].UpdateEffect(effects[i].Amount);
                    break;

                case TeamEffectsType.Shooter:
                    TeamEffectsList[5].EnableEffect();
                    TeamEffectsList[5].UpdateEffect(effects[i].Amount);
                    break;

                case TeamEffectsType.Magic:
                    TeamEffectsList[6].EnableEffect();
                    TeamEffectsList[6].UpdateEffect(effects[i].Amount);
                    break;

                case TeamEffectsType.Aristocratism:
                    TeamEffectsList[7].EnableEffect();
                    TeamEffectsList[7].UpdateEffect(effects[i].Amount);
                    break;
            }
        }
    }

    private void TurnOffEffectsBeforeUpdate()
    {
        for (int i = 0; i < TeamEffectsList.Count; i++)
        {
            TeamEffectsList[i].DisableEffect();
        }
    }
}

public enum TeamEffectsType
{
    Pirate = 1,
    Vampire = 2,
    Snow = 3,
    Alchemist = 4,
    Viking = 5,
    Shooter = 6,
    Magic = 7,
    Aristocratism = 8
}

public class TeamEffectsData
{
    public TeamEffectsType EffectType;
    public int Amount = 0;

    public TeamEffectsData(TeamEffectsType effectType) => EffectType = effectType;

    public void AddValue(int value)
    {
        Amount += value;
    }
}
