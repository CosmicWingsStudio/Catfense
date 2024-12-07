using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class SellUnitGUIHandler : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler

{
    [Inject] private ShopHandler _shopHandler;
    [SerializeField] private Button _sellButton;
    [SerializeField] private string _constantSellText;    
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
        _object.transform.position = Camera.main.WorldToScreenPoint(_currentUnit.transform.position);
        //_object.transform.localPosition = Input.mousePosition;
        _sellPriceText.text = _currentUnit.SellPrice.ToString() + " " + _constantSellText;
    }
    
    public void CloseSellUnitScreen()
    {
        _object.SetActive(false);
    }

    private void SendSellRequest()
    {
        _shopHandler.SellUnit(_currentUnit);
        CloseSellUnitScreen();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CloseSellUnitScreen();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }
}
