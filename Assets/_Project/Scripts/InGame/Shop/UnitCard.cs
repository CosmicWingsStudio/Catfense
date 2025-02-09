using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class UnitCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler    
{
    private UnitConfig _config;

    [SerializeField] private string _constantPurchaseText;
    [SerializeField] private GameObject _purchasePanel;
    [SerializeField] private GameObject _descriptionPanel;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _damage;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _typeOfAttack;
    [SerializeField] private Image _unitSpriteImage;

    private int _price;

    [Inject] private ShopHandler _shopHandler;

    private TextMeshProUGUI _purchaseText;

    public void SetConfig(UnitConfig config)
    {
        _config = config;

        _unitSpriteImage.sprite = _config.UnitSprite;
        _price = _config.Price;
        _typeOfAttack.text = "Тип атаки: " + _config.UnitAttackType;
        _buyButton.onClick.AddListener(SendPurchaseRequest);
        _damage.text = _config.Damage.ToString();
        _health.text = _config.HealthPoints.ToString();
        _name.text = _config.PresentiveName.ToString();
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

    private void SendPurchaseRequest() => _shopHandler.Purchase(_config, gameObject, _price);
}
