using System.Collections.Generic;
using UnityEngine;

public class StorageBarHandler : MonoBehaviour
{
    [SerializeField, Tooltip("Put a folder with slots for adding them to private slots array")]
    private Transform _slotsFolder;

    private List<StorageSlot> _slots = new();

    private void Start()
    {
        for (int i = 0; i < _slotsFolder.childCount; i++)
        {
            _slots.Add(_slotsFolder.GetChild(i).GetComponent<StorageSlot>());
        }
    }

    public bool AllSlotsAreFullOrNot()
    {
        foreach (StorageSlot sSlot in _slots)
        {
            if(sSlot.Item == null)
                return false;
        }
        return true;
    }

    public StorageSlot FindFreeSlot()
    {
        foreach (StorageSlot sSlot in _slots)
        {
            if (sSlot.Item == null)
                return sSlot;
        }
        return null;
    }
}
