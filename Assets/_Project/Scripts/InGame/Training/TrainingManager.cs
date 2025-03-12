
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class TrainingManager : MonoBehaviour
{
    public static TrainingManager Instance;

    public List<bool> Steps = new();
    public List<Button> Buttons = new();
    [ReadOnly] public int CurrentStep = 0;
    [Inject] private SignalBus _signalBus;

    [Header("General references")]
    [SerializeField] private EnvironmentHandler _environmentHandler;
    [SerializeField] private UnitDragPlacer _unitDragPlacer;
    [SerializeField] private WalletHandler _walletHandler;
    [SerializeField] private ShopHandler _shopHandler;

    [Header("0 Step")]
    [SerializeField] private Button _triggerButton0;
    [SerializeField] private GameObject _object0;
    [Header("1 Step")]
    [SerializeField] private Button _triggerButton1;
    [SerializeField] private GameObject _object1;
    [Header("2 Step")]
    [SerializeField] private Button _triggerButton2;
    [SerializeField] private GameObject _object2;
    private bool IsGiven2 = false;
    [Header("3 Step")]
    [SerializeField] private Button _triggerButton3;
    [SerializeField] private GameObject _object3;
    [Header("4 Step")]
    [SerializeField] private Button _triggerButton4;
    [SerializeField] private GameObject _object4;
    private bool IsGiven4 = false;
    [Header("5 Step")]
    [SerializeField] private Button _triggerButton5;
    [SerializeField] private GameObject _object5;
    private bool IsGiven5 = false;
    [Header("6 Step")]
    [SerializeField] private Button _triggerButton6;
    [SerializeField] private GameObject _object6;
    private bool IsGiven6 = false;
    [Header("7 Step")]
    [SerializeField] private Button _triggerButton7;
    [SerializeField] private GameObject _object7;
    [Header("8 Step")]
    [SerializeField] private Button _triggerButton8;
    [SerializeField] private GameObject _object8;
    private bool IsGiven8 = false;
    [Header("9 Step")]
    [SerializeField] private Button _triggerButton9;
    [SerializeField] private GameObject _object9;
    private bool IsGiven9 = false;

    private bool IsReadyToFinish = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        } 
    }

    private void Start()
    {
        _unitDragPlacer.IsDisabledBySellScreen = true;
        _shopHandler.SellingDisable = true;
        DisableAllButtons();

        _signalBus.Subscribe<LevelEndedSignal>(ctx => HandleLevelEnd(ctx.ResultType));

        _triggerButton0.onClick.AddListener(CompleteCurrentStep);
        _triggerButton1.onClick.AddListener(CompleteCurrentStep);
        _triggerButton2.onClick.AddListener(CompleteCurrentStep);
        _triggerButton3.onClick.AddListener(CompleteCurrentStep);
        _triggerButton4.onClick.AddListener(CompleteCurrentStep);
        _triggerButton5.onClick.AddListener(CompleteCurrentStep);
        _triggerButton6.onClick.AddListener(CompleteCurrentStep);
        _triggerButton7.onClick.AddListener(CompleteCurrentStep);
        _triggerButton8.onClick.AddListener(CompleteCurrentStep);
        _triggerButton9.onClick.AddListener(CompleteCurrentStep);
    }

    private void Update()
    {
        switch (CurrentStep)
        {
            case 0:
                _object0.SetActive(true);
                break;
            case 1:
                _object0.SetActive(false);
                _object1.SetActive(true);
                break;
            case 2:
                _object1.SetActive(false);
                _object2.SetActive(true);
                if (!IsGiven2)
                {
                    _walletHandler.AddMoney(20);
                    IsGiven2 = true;
                } 

                break;
            case 3:
                _object2.SetActive(false);
                _object3.SetActive(true);
                break;

            case 4:
                _object3.SetActive(false);
                _object4.SetActive(true);

                if (!IsGiven4)
                {
                    _unitDragPlacer.IsDisabledBySellScreen = true;
                    _shopHandler.SellingDisable = false;
                    IsGiven4 = true;
                }

                break;

            case 5:
                _object4.SetActive(false);
                _object5.SetActive(true);

                if (!IsGiven5)
                {
                    _unitDragPlacer.IsDisabledBySellScreen = true;
                    _walletHandler.AddMoney(100);
                    IsGiven5 = true;
                }
                break;

            case 6:
                _object5.SetActive(false);
                _object6.SetActive(true);
                if (!IsGiven6)
                {
                    _unitDragPlacer.IsDisabledBySellScreen = false;
                    IsGiven6 = true;
                }
                break;

            case 7:
                _object6.SetActive(false);
                _object7.SetActive(true);
                break;

            case 8:
                _object7.SetActive(false);
                _object8.SetActive(true);

                if (!IsGiven8)
                {
                    _walletHandler.AddMoney(250);
                    IsGiven8 = true;
                }

                break;

            case 9:
                _object8.SetActive(false);

                if (!IsGiven9)
                {
                    EnableAllButtons();
                    _unitDragPlacer.IsDisabledBySellScreen = false;
                    IsGiven9 = true;
                }

                if(IsReadyToFinish)
                    _object9.SetActive(true);
                break;

            default:
                break;
        }
    }

    public void CompleteCurrentStep()
    {
        bool check = false;

        switch (CurrentStep)
        {
            case 0:
                check = true;
                break;

            case 1:
                List<BenchSlot> slots = _environmentHandler.GetEnvironmentContainer().BenchSlots;
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i].Item != null)
                    {
                        check = true;
                        break;
                    }
                }
                break;

            case 2:
                check = true;
                break;

            case 3:
                List<PlaceSlot> pslots = _environmentHandler.GetEnvironmentContainer().GetAllPlaceableSlots();
                for (int i = 0; i < pslots.Count; i++)
                {
                    if (pslots[i].Item != null)
                    {
                        check = true;
                        break;
                    }
                }
                break;

            case 4:
                check = true;
                _shopHandler.SellingDisable = true;
                break;

            case 5:
                var listRef = _environmentHandler.GetEnvironmentContainer().BuyableTowerParts;
                if (listRef.Count == 0)
                    check = true;
                break;
            case 6:
                check = true;
                break;
            case 7:
                check = true;
                break;
            case 8:
                check = true;
                break;
            case 9:
                check = true;
                break;

        }

        if (check)
        {
            Steps[CurrentStep] = true;
            if(CurrentStep == 9)
                SceneManager.LoadScene("InMenuScene");

            if (CurrentStep + 1 <= Steps.Count)
                CurrentStep++;
        }    
    }

    private void HandleLevelEnd(ResultType result)
    {
        if(result == ResultType.Win)
        {
            IsReadyToFinish = true;
            _unitDragPlacer.IsDisabledBySellScreen = true;
        }
        else
        {
            SceneManager.LoadScene("InGameTrainingScene");
        }
        
    }

    private void DisableAllButtons()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            Buttons[i].interactable = false;
        }
    }

    private void EnableAllButtons()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            Buttons[i].interactable = true;
        }
    }
}
