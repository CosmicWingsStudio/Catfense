using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class UnitCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler    
{
    [field:SerializeField] public UnitConfig Config { get; private set; }

    [SerializeField] private int _price;
    [SerializeField] private string _constantPurchaseText;
    [SerializeField] private GameObject _purchasePanel;
    [SerializeField] private GameObject _descriptionPanel;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _damage;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _name;

    [Inject] private ShopHandler _shopHandler;

    private TextMeshProUGUI _purchaseText;

    private void Start()
    {
        _buyButton.onClick.AddListener(SendPurchaseRequest);
        _damage.text = Config.Damage.ToString();
        _health.text = Config.HealthPoints.ToString();
        _name.text = Config.Name.ToString();
        _purchaseText = _buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _purchasePanel.SetActive(true);
        _descriptionPanel.SetActive(false);
        _purchaseText.text = _price.ToString() + " " +_constantPurchaseText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _purchasePanel.SetActive(false);
        _descriptionPanel.SetActive(true);
    }
    private void SendPurchaseRequest() => _shopHandler.Purchase(Config, gameObject, _price);
}
