using TMPro;
using UnityEngine;
using Zenject;

public class WalletHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private int _startMoney = 50;

    public int CurrentMoney { get; private set; }

    private void Start()
    {
        CurrentMoney = _startMoney;
        _moneyText.text = CurrentMoney.ToString();
    }

    public void SpendMoney(int amount)
    {
        CurrentMoney -= amount;
        _moneyText.text = CurrentMoney.ToString();
    }

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        _moneyText.text = CurrentMoney.ToString();
    }
}
