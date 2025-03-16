using UnityEngine;

public class TeamEffect_Magic : TeamEffect_Unit
{
    [SerializeField] private SpawningSpellButton _spellButton1;
    [SerializeField] private ActivatingSpellButton _spellButton2;

    protected override void ApplyEffect()
    {
        switch (_currentNumber)
        {
            case 2:
                _spellButton1.BonusCooldownReduce = _step1;
                _spellButton1.BonusDamage = _step1 + (_step1 * 0.5f);
                _spellButton2.BonusCooldownReduce = _step1;
                break;
            case 3:
                _spellButton1.BonusCooldownReduce = _step2;
                _spellButton1.BonusDamage = _step1 + (_step1 * 1.0f);
                _spellButton2.BonusCooldownReduce = _step2;
                break;
            case 4:
                _spellButton1.BonusCooldownReduce = _step3;
                _spellButton1.BonusDamage = _step1 + (_step1 * 1.5f);
                _spellButton2.BonusCooldownReduce = _step3;
                break;
            default:
                _spellButton1.BonusCooldownReduce = 0;
                _spellButton2.BonusCooldownReduce = 0;
                break;
        }
    }
}
