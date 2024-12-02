using UnityEngine;
using Zenject;

public class WalletHandler : MonoBehaviour
{
    public int CurrentMoney { get; private set; }

    //[Inject]
    //private void Initialize()
    //{

    //}

    public void SpendMoney(int amount) => CurrentMoney -= amount;

    public void AddMoney(int amount) => CurrentMoney += amount;
}
