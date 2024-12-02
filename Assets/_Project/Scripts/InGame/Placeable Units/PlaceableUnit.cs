
using UnityEngine;
using Zenject;

public class PlaceableUnit : MonoBehaviour
{
    public bool IsBenched
    {
        get
        {
            if(transform.parent != null && transform.parent.gameObject.GetComponent<BenchSlot>())
                return true;
            else
                return false;
        }
        set{}
    }
    public bool IsPlaced
    {
        get
        {
            if (transform.parent != null && transform.parent.gameObject.GetComponent<PlaceSlot>())
                return true;
            else
                return false;
        }
        set {}
    }
    public Slot2D ParentSlot
    {
        get
        {
            if (transform.parent != null && transform.parent.gameObject.TryGetComponent(out Slot2D slot))
                return slot;
            else
                return null;
        }
    }

    [Inject] private EnvironmentHandler _environmentHandler;

    private int _clickNumber = 0;
    private float _clickTime = 0f;
    private float _clickDelay = 0.5f;

    private float _dragStateTreshold = 0.1f;
    private float _holdingTimer;
    private bool IsDragging = false;
    private bool IsOnDoubleClickFrame = false;

    private Vector2 _originalPosition;

    private void OnMouseDown()
    { 
        _originalPosition = Vector2.zero;

        DoubleClickChecker();
    }

    private void OnMouseDrag()
    {
        if (_holdingTimer < _dragStateTreshold)
            _holdingTimer += Time.deltaTime;
        else
        {
            if (IsOnDoubleClickFrame)
                return;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);          
            IsDragging = true;
        }
        
    }

    private void OnMouseUp()
    {
        _holdingTimer = 0f;
        if (IsOnDoubleClickFrame)
            IsOnDoubleClickFrame = false;

        if (!IsDragging)
            return;

        if(IsDragging)
            IsDragging = false;

        var typeofslot = ParentSlot;

        if (typeofslot == null)
            Debug.LogError("Slot is missing");

        if (TryToSetIntoDefinedSlot() == false)
            BackToOriginalSlot();
    }

    private void BackToOriginalSlot()
    {
        transform.localPosition = _originalPosition;
        Debug.Log("orig slot");
    }

    private bool TryToSetIntoDefinedSlot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.RaycastAll(mousePosition, Vector2.right);

        foreach (var collider in hit)
        {
            if (collider.transform.TryGetComponent(out Slot2D Slot))
            {
                if (Slot != ParentSlot)
                {
                    ParentSlot.InformOfTakingItemFromSlot();
                    Slot.PlaceItemInSlot(transform);
                    return true;
                }
                
            }
        }

        return false;
    }

    private void DoubleClickChecker()
    {
        _clickNumber++;

        if (_clickNumber == 1)
            _clickTime = Time.time;

        if (_clickNumber > 1 && Time.time - _clickTime <= _clickDelay)
        {
            _clickNumber = 0;
            _clickTime = 0;
            DoubleClickAction();
        }
        else if (Time.time - _clickTime > 1)
            _clickNumber = 0;
    }

    private void DoubleClickAction()
    {  
        IsOnDoubleClickFrame = true;
        if (ParentSlot is PlaceSlot)
        {
            var oldParentSlot = ParentSlot;
            if (_environmentHandler.GetEnvironmentContainer().TrySetItemInBenchSlot(transform))
                oldParentSlot.InformOfTakingItemFromSlot();
        }
    }
}
