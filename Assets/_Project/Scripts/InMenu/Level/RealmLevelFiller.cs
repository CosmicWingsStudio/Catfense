using TMPro;
using UnityEngine;

[RequireComponent(typeof(RealmLevelInfo))]
public class RealmLevelFiller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _wavesAmount;
    [SerializeField] private TextMeshProUGUI _levelOrder;


    private RealmLevelInfo _levelInfo;
    private void Start()
    {
        _levelInfo = GetComponent<RealmLevelInfo>();
    }

    private void FillUIElementsWithInformation()
    {
        
    }
}
