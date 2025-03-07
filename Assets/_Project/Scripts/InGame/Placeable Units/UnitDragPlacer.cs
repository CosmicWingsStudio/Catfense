
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UnitDragPlacer : MonoBehaviour
{
    public bool IsDragging
    {
        get
        {
            if (_currentDraggableUnit != null)
                return true;
            else
                return false;
        }   
    }

    public bool IsDisabledBySellScreen = false;

    private EnvironmentHandler _environmentHandler;
    private SignalBus _signalBus;
    private GUIWarningHandler _guiWarningHandler;

    private bool IsDragSystemAvailable = true;
    private bool IsPaused = false;
    private bool _isDragging = false;
    private PlaceableUnit _currentDraggableUnit; 

    private int _clickNumber = 0;
    private float _clickTime = 0f;
    private float _clickDelay = 0.5f;

    [SerializeField] private float _dragStateTreshold = 0.05f; //0.1 stable option, but a delay is seen a little bit
    private float _holdingTimer;
    private bool IsOnDoubleClickFrame = false;

    private bool IsStartDraggingSoundReady = false;

    [Inject]
    private void Initialize(EnvironmentHandler environmentHandler, SignalBus signalBus, GUIWarningHandler gUIWarningHandler)
    {
        _environmentHandler = environmentHandler;
        _signalBus = signalBus;
        _guiWarningHandler = gUIWarningHandler;

        _signalBus.Subscribe<WaveStartedSignal>(HandleUnitsDraggableState);
        _signalBus.Subscribe<WaveEndedSignal>(HandleUnitsDraggableState);
        _signalBus.Subscribe<PausedSignal>(() => IsPaused = true);
        _signalBus.Subscribe<UnpausedSignal>(() => IsPaused = false);
    }

    private void Update()
    {
        if (IsPaused || IsDisabledBySellScreen)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (!IsDragSystemAvailable && VerifyUnit())
            {
                _guiWarningHandler.ShowWarningScreen("Нельзя перемещать юниты во время боя");
                return;
            }
            else
                DownClickHandler();
        }

        if (!IsDragSystemAvailable)
        {
            if (_currentDraggableUnit != null)
            {
                BackToOriginalSlot();
                _currentDraggableUnit.DataDisplayer.TurnOnDisplayOnTheEndOfDragging();
                //SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.SoundUnitPickedUp);

                if(_currentDraggableUnit.IsBenched)
                    _currentDraggableUnit.TurnOffActiveMode();
                else
                    _currentDraggableUnit.TurnOnActiveMode();

                List<PlaceSlot> slots = _environmentHandler.GetEnvironmentContainer().GetAllPlaceableSlots();
                for (int i = 0; i < slots.Count; i++)
                {
                    slots[i].HideDropZone();
                }

                _currentDraggableUnit = null;
            }
            return;
        }

        if (_currentDraggableUnit != null)
        {
            DragClickHandler();  
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (_currentDraggableUnit == null)
                return;

            UpClickHandler();
        }
    }

   
    #region Click Handlers

    private void DownClickHandler()
    {
        if (GetUnit() != null)
        {
            _currentDraggableUnit = GetUnit();
            IsOnDoubleClickFrame = false;
            DoubleClickChecker();

            IsStartDraggingSoundReady = true;

            _currentDraggableUnit.DataDisplayer.TurnOffDisplayWhileDragging();
            _currentDraggableUnit.TurnOffActiveMode(true);
            _currentDraggableUnit.spriteRenderer.sortingOrder = _currentDraggableUnit.DefaultSortingOrder + 5;
            _isDragging = true;

            SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.SoundUnitPickedUp);

            List<PlaceSlot> slots = _environmentHandler.GetEnvironmentContainer().GetAllPlaceableSlots();

            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].ShowDropZone();
            }
        }
    }

    private void UpClickHandler()
    {
        _holdingTimer = 0f;

        if (!_isDragging)
        {
            _currentDraggableUnit = null;
            IsOnDoubleClickFrame = false;
            return;
        }

        if (_isDragging)
        {
            _currentDraggableUnit.DataDisplayer.TurnOnDisplayOnTheEndOfDragging();
            _currentDraggableUnit.spriteRenderer.sortingOrder = _currentDraggableUnit.DefaultSortingOrder;
            if(_currentDraggableUnit.ParentSlot.GetComponent<PlaceSlot>())
                _currentDraggableUnit.TurnOnActiveMode();
            _isDragging = false;

            List<PlaceSlot> slots = _environmentHandler.GetEnvironmentContainer().GetAllPlaceableSlots();
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].HideDropZone();
            }
        }

        if (IsOnDoubleClickFrame)
        {
            IsOnDoubleClickFrame = false;
            _currentDraggableUnit = null;
            return;
        }

        if (DefineParentSlot(_currentDraggableUnit.transform) == null)
            Debug.LogError("Slot is missing");

        if (TryToSetIntoDefinedSlot() == false)
            BackToOriginalSlot();
        else
            SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.SoundUnitPlacement);

        _currentDraggableUnit = null;
    }

    private void DragClickHandler()
    {
        if (_holdingTimer < _dragStateTreshold)
        {
            _holdingTimer += Time.deltaTime;
            
        }
        else
        {
            if (IsOnDoubleClickFrame) return;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _currentDraggableUnit.transform.position;
            _currentDraggableUnit.transform.Translate(mousePosition);
            
        }
    }

    private void DoubleClickChecker()
    {
        if (Time.time - _clickTime > _clickDelay)
        {
            _clickNumber = 0;
        }
        _clickNumber++;
        if (_clickNumber == 1)
            _clickTime = Time.time;

        if (_clickNumber > 1 && Time.time - _clickTime <= _clickDelay)
        {
            _clickNumber = 0;
            _clickTime = 0;
            DoubleClickAction();
        }
        
    }

    private void DoubleClickAction()
    {
        IsOnDoubleClickFrame = true;
        if (DefineParentSlot(_currentDraggableUnit.transform).GetComponent<PlaceSlot>())
        {
            var oldParentSlot = DefineParentSlot(_currentDraggableUnit.transform);
            if (_environmentHandler.GetEnvironmentContainer().TrySetItemInBenchSlot(_currentDraggableUnit.transform))
            {
                oldParentSlot.InformOfTakingItemFromSlot();
                SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.SoundUnitPlacement);
            }
        }
    }

    #endregion

    private PlaceableUnit GetUnit()
    {
        if (_currentDraggableUnit != null)
            return null;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.RaycastAll(mousePos, Vector2.zero);
        foreach (var collider in hit)
        {

            if (collider.transform.TryGetComponent(out PlaceableUnit pUnit))
            {
                if (pUnit.OnSaleScreen)
                    return null;
                else 
                {
                    return pUnit;
                }
            }
        }

        return null;
    }

    private bool VerifyUnit()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.RaycastAll(mousePos, Vector2.right);
        foreach (var collider in hit)
        {
            if (collider.transform.GetComponent<PlaceableUnit>())
                return true;
        }

        return false;
    }

    private void BackToOriginalSlot()
    {
        _currentDraggableUnit.transform.localPosition = _currentDraggableUnit.ParentSlot.TakeOriginalPosition();
    }

    private bool TryToSetIntoDefinedSlot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) /*- _currentDraggableUnit.transform.position*/;
        var hit = Physics2D.RaycastAll(mousePosition, Vector2.right, 0f);
        foreach (var collider in hit)
        {
            if (collider.transform.TryGetComponent(out Slot2D Slot))
            {
                var definedSlot = DefineParentSlot(_currentDraggableUnit.transform);
                if (Slot != definedSlot)
                {
                    if (definedSlot is BenchSlot && Slot is BenchSlot)
                        return false;

                    definedSlot.InformOfTakingItemFromSlot();
                    Slot.PlaceItemInSlot(_currentDraggableUnit.transform);
                    return true;
                }

            }
            else if(collider.transform.TryGetComponent(out PlaceableUnit pUnit))
            {
                if(pUnit != _currentDraggableUnit)
                    SwapUnitsPosition(pUnit);
            }
        }

        return false;
    }

    private void SwapUnitsPosition(PlaceableUnit pUnit)
    {
        Slot2D Slot1 = _currentDraggableUnit.ParentSlot;
        Slot2D Slot2 = pUnit.ParentSlot;

        Slot1.SwapItemWithAnotherItem(pUnit.transform);
        Slot2.SwapItemWithAnotherItem(_currentDraggableUnit.transform);
    }

    private Slot2D DefineParentSlot(Transform obj)
    {
        if (obj == null)
            return null;

        if (obj.parent != null && obj.parent.gameObject.TryGetComponent(out Slot2D slot))
            return slot;
        else
            return null;
    }

    private void HandleUnitsDraggableState()
    {
        if (IsDragSystemAvailable)
            IsDragSystemAvailable = false;
        else
            IsDragSystemAvailable = true;
    }
}
