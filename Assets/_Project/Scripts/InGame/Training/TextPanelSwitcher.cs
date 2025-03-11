
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _object2Close;
    [SerializeField] private GameObject _object2Open;
    [Header("Additional")]
    [SerializeField] private UnitDragPlacer _unitDragger;
    [SerializeField] private bool state = false;
    [SerializeField] private EnvironmentHandler _environmentHandler;
    [SerializeField] private bool CheckAnyUnitsOnPlaceSlots = false;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(ManageClick);
    }

    private void ManageClick()
    {
        if (CheckAnyUnitsOnPlaceSlots)
        {
            bool status = false;
            List<PlaceSlot> slots = _environmentHandler.GetEnvironmentContainer().GetAllPlaceableSlots();
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].Item != null)
                {
                    status = true;
                    break;
                }
                    
            }

            if (!status)
                return;
        }
        _object2Close.SetActive(false);
        _object2Open.SetActive(true);
        if (_unitDragger != null)
            _unitDragger.IsDisabledBySellScreen = state;
    }
}
