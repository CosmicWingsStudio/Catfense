using UnityEngine;

public class TeamEffect_Pirate : TeamEffect_Unit
{
    [SerializeField] private WalletHandler _wallet;

    protected override void ApplyEffect()
    {
        switch(_currentNumber)
        {
            case 2:
                _wallet.AdditionalMoneyBonus = _step1;
                break;
            case 3:
                _wallet.AdditionalMoneyBonus = _step2;
                break;
            case 4:
                _wallet.AdditionalMoneyBonus = _step3;
                break;
            default:
                _wallet.AdditionalMoneyBonus = 0;
                break;
        }
    }

}
