using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitDataDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Transform _starsFolder;

    [field:SerializeField] public Slider _hpSlider { get; private set; }

    public string UnitName { get; set; }
    private List<GameObject> _stars = new();
    private int _starsIterator = 0;

    private void Start()
    {
        for (int i = 0; i < _starsFolder.childCount; i++)
        {
            _stars.Add(_starsFolder.GetChild(i).gameObject);
        }
    }

    public void SetDisplayInformation()
    {
        _nameText.text = UnitName;
    }

    public void ShowUnitUpgrade()
    {
        if(_starsIterator < _stars.Count)
        {
            _stars[_starsIterator].SetActive(true);
            _starsIterator++;
        }
    }

    public void TurnOffDisplayWhileDragging()
    {
        _starsFolder.gameObject.SetActive(false);
        _hpSlider.gameObject.SetActive(false);

    }

    public void TurnOnDisplayOnTheEndOfDragging()
    {
        _starsFolder.gameObject.SetActive(true);
        _hpSlider.gameObject.SetActive(true);
    }
}
