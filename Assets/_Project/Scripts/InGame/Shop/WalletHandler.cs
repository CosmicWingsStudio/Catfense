using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class WalletHandler : MonoBehaviour
{
    [SerializeField] private RestartLevelDataSaver _restartLevelDataSaver;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _incomeMoneyText;
    [SerializeField] private TextMeshProUGUI _outcomeMoneyText;
    [SerializeField] private float _moneyHighlightingTime = 0.90f;
    [Inject] private SignalBus _signalBus;

    private int _startMoney;
    private int _moneyPerWave;
    private Vector3 _defaultChangedOutcomeMoneyTextPosition;
    private Vector3 _defaultChangedIncomeMoneyTextPosition;
    private float _defaultFontSize;
    private Color _defaultTextColor;
    private bool AddHighlighting = false;
    private bool ReduceHighlighting = false;
    private bool IsHighlightingFinished = false;
    private bool IsInitialised = false;

    public int CurrentMoney { get; private set; }

    public float AdditionalMoneyBonus { get; set; }

    public void Initialize(int startMoney, int moneyFromWaveEnd)
    {
        if(IsInitialised == false)
        {
            IsInitialised = true;

            _startMoney = startMoney;
            _moneyPerWave = moneyFromWaveEnd;

            CurrentMoney = _startMoney;

            if (_restartLevelDataSaver.OnRestart)
            {
                CurrentMoney += _restartLevelDataSaver.AdditionalMoney;
                _restartLevelDataSaver.AdditionalMoney = 0;
            }

            _moneyText.text = CurrentMoney.ToString();
            _defaultFontSize = _moneyText.fontSize;
            _defaultTextColor = _moneyText.color;
            _signalBus.Subscribe<WaveEndedSignal>(GetAfterWaveMoney);
            _defaultChangedOutcomeMoneyTextPosition = _outcomeMoneyText.transform.position;
            _defaultChangedIncomeMoneyTextPosition = _incomeMoneyText.transform.position;
        }
    }

    private void Update()
    {
        if (AddHighlighting)
        {
            AddMoneyTextHighlight();
        }
        else if (ReduceHighlighting)
        {
            ReduceMoneyTextHighlight();
        }
        else if (IsHighlightingFinished)
        {
            MoneyTextDehighlight();
        }
    }

    public void SpendMoney(int amount, int actionSound = 1)
    {
        CurrentMoney -= amount;
        _moneyText.text = CurrentMoney.ToString();
        ReduceHighlighting = true;

        Vector3 newPos = new(_defaultChangedOutcomeMoneyTextPosition.x + Random.Range(-15, 15),
           _defaultChangedOutcomeMoneyTextPosition.y);
        _outcomeMoneyText.transform.position = newPos;
        _outcomeMoneyText.text = "-" + amount.ToString();
        StartCoroutine(OutcomeMoneyShow());

        switch (actionSound)
        {
            case 1:
                SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.PurchaseButtonClick);
                break;
            case 2:
                SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.BuildButtonClick);
                break;
            default:
                SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.PurchaseButtonClick);
                break;
        }
       
    }

    public void AddMoney(int amount, bool disableAdditionalMoneyBonus = false)
    {
        int NewAmount = amount;

        if (disableAdditionalMoneyBonus == true)
            CurrentMoney += amount;
        else
        {
            int Bonus = (int)(amount * AdditionalMoneyBonus);
            NewAmount = amount + Bonus;
            CurrentMoney += NewAmount;
        }
        _moneyText.text = CurrentMoney.ToString();
        AddHighlighting = true;

        Vector3 newPos = new(_defaultChangedIncomeMoneyTextPosition.x + Random.Range(-15, 15),
            _defaultChangedIncomeMoneyTextPosition.y);
        _incomeMoneyText.transform.position = newPos;
        _incomeMoneyText.text = "+" + NewAmount.ToString();
        StartCoroutine(IncomeMoneyShow());
    }

    private void GetAfterWaveMoney()
    {
        AddMoney(_moneyPerWave);
    }

    private IEnumerator IncomeMoneyShow()
    {
        _incomeMoneyText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.25f);
        _incomeMoneyText.gameObject.SetActive(false);
    }

    private IEnumerator OutcomeMoneyShow()
    {
        _outcomeMoneyText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.25f);
        _outcomeMoneyText.gameObject.SetActive(false);
    }

    private void AddMoneyTextHighlight()
    {   
        if(_moneyText.fontSize + 0.1f > _defaultFontSize + 2.5f)
        {
            AddHighlighting = false;
            IsHighlightingFinished = true;
        }
        else
        {
            float step = 0.15f / _moneyHighlightingTime;
            _moneyText.fontSize = Mathf.Lerp(_moneyText.fontSize, _moneyText.fontSize + 2.5f, step);
            _moneyText.color = Color.Lerp(_moneyText.color, Color.yellow, step);
        }
    }

    private void ReduceMoneyTextHighlight()
    {
        if (_moneyText.fontSize + 0.1f > _defaultFontSize + 2.5f)
        {
            ReduceHighlighting = false;
            IsHighlightingFinished = true;
        }
        else
        {
            float step = 0.15f / _moneyHighlightingTime;
            _moneyText.fontSize = Mathf.Lerp(_moneyText.fontSize, _moneyText.fontSize + 2.5f, step);
            _moneyText.color = Color.Lerp(_moneyText.color, Color.red, step);
        }
    }

    private void MoneyTextDehighlight()
    {       

        if (_moneyText.fontSize - 0.1f < _defaultFontSize)
            IsHighlightingFinished = false;
        else
        {
            float step = 0.10f / _moneyHighlightingTime;
            _moneyText.fontSize = Mathf.Lerp(_moneyText.fontSize, _defaultFontSize, step);
            _moneyText.color = Color.Lerp(_moneyText.color, _defaultTextColor, step);
        }
    }
}
