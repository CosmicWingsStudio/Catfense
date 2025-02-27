using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class SellUnitGUIHandler : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler

{
    [Inject] private ShopHandler _shopHandler;
    [Inject] private UnitDragPlacer _unitDragPlacer;
    [SerializeField] private Button _sellButton;
    [SerializeField] private string _constantSellText;
    [SerializeField] private float _offset;
    private GameObject _object;
    private TextMeshProUGUI _sellPriceText;
    private PlaceableUnit _currentUnit;

    private void Start()
    {
        _sellPriceText = _sellButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _object = transform.GetChild(0).gameObject;
        _sellButton.onClick.AddListener(SendSellRequest);
    }
    public void ShowSellUnitScreen(PlaceableUnit currentUnit)
    {
        _object.SetActive(true);
        _currentUnit = currentUnit;
        Vector3 pos = Camera.main.WorldToScreenPoint(_currentUnit.transform.position);
        pos.y += _offset;
        _object.transform.position = pos;
        _sellPriceText.text = _currentUnit.SellPrice.ToString() + " " + _constantSellText;
    }
    
    public void CloseSellUnitScreen()
    {
        _currentUnit.OnSaleScreen = false;
        _object.SetActive(false);
    }

    private void SendSellRequest()
    {
        _shopHandler.SellUnit(_currentUnit);
        CloseSellUnitScreen();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _unitDragPlacer.IsDisabledBySellScreen = false;
        CloseSellUnitScreen();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _unitDragPlacer.IsDisabledBySellScreen = true;
    }
}
