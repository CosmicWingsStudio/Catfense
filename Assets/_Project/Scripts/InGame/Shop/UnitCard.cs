using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class UnitCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler    
{
    [field:SerializeField] public UnitConfig Config { get; private set; }

    [SerializeField] private int _price;
    [SerializeField] private GameObject _purchasePanel;
    [SerializeField] private GameObject _descriptionPanel;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _damage;
    [SerializeField] private TextMeshProUGUI _health;

    [Inject] private ShopHandler _shopHandler;

    private void Start()
    {
        _buyButton.onClick.AddListener(SendPurchaseRequest);
        _damage.text = Config.Damage.ToString();
        _health.text = Config.HealthPoints.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _purchasePanel.SetActive(true);
        _descriptionPanel.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _purchasePanel.SetActive(false);
        _descriptionPanel.SetActive(true);
    }



    private void SendPurchaseRequest() => _shopHandler.Purchase(_price, Config.PrefabName, gameObject);
}
