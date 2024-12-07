using TMPro;
using UnityEngine;
using Zenject;

public class WalletHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;

    public int CurrentMoney { get; private set; }

    private void Start() => _moneyText.text = CurrentMoney.ToString();

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
