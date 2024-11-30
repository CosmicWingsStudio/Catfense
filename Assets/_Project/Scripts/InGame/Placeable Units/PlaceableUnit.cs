
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

    //[Inject] private EnvironmentHandler _environmentHandler;
    [SerializeField] private EnvironmentContainerHandler _environmentHandler;

    private int _clickNumber = 0;
    private float _clickTime = 0f;
    private float _clickDelay = 0.5f;
    private bool OnDoubleClick = false;

    private Vector2 _originalPosition;

    private void OnMouseDown()
    { 
        _originalPosition = Vector2.zero;

        DoubleClickChecker();
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePosition);
    }

    private void OnMouseUp()
    {
        if (OnDoubleClick)
        {
            OnDoubleClick = false;
            Debug.Log("cap worked");
            return;
        }
            

        var typeofslot = ParentSlot;

        if (typeofslot == null)
            Debug.LogError("Slot is missing");

        if (typeofslot is BenchSlot)
        {
            if(TryToSetIntoPlaceSlot() == false)
                BackToOriginalSlot();
        }
        else if(typeofslot is PlaceSlot)
        {
            if (TryToSetIntoBenchSlot() == false)
                BackToOriginalSlot();
        }


    }

    private void BackToOriginalSlot()
    {
        transform.localPosition = _originalPosition;
        Debug.Log("Backt to orig");
    }

    private bool TryToSetIntoPlaceSlot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.RaycastAll(mousePosition, Vector2.right);

        foreach (var collider in hit)
        {
            if(collider.transform.TryGetComponent(out PlaceSlot placeSlot))
            {
                ParentSlot.InformOfTakingItemFromSlot();
                placeSlot.PlaceItemInSlot(transform);
                return true;
            }
        }

        return false;
    }

    private bool TryToSetIntoBenchSlot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.RaycastAll(mousePosition, Vector2.right);

        foreach (var collider in hit)
        {
            if (collider.transform.TryGetComponent(out BenchSlot benchSlot))
            {
                ParentSlot.InformOfTakingItemFromSlot();
                benchSlot.PlaceItemInSlot(transform);
                return true;
            }
        }

        return false;
    }

    private void DoubleClickChecker()
    {
        _clickNumber++;
        if (_clickNumber == 1)
            _clickTime = Time.time;

        if (_clickNumber > 1 && Time.time - _clickTime < _clickDelay)
        {
            _clickNumber = 0;
            _clickTime = 0;
            OnDoubleClick = true;
            DoubleClickAction();
        }
        else if (_clickNumber > 2 || Time.time - _clickTime > 1)
            _clickNumber = 0;
    }

    private void DoubleClickAction()
    {
        //if(ParentSlot is PlaceSlot)
        //    _environmentHandler.GetEnvironmentContainer().TrySetItemInBenchSlot(transform);
        if (ParentSlot is PlaceSlot)
        {
            var oldParentSlot = ParentSlot;
            Debug.Log(ParentSlot);
            if (_environmentHandler.TrySetItemInBenchSlot(transform))
                oldParentSlot.InformOfTakingItemFromSlot();
        }
    }
}
