using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamEffect : MonoBehaviour
{
    private int _numbers;

    public int Numbers
    {
        get { return _numbers; }
        set
        {
            _numbers = value;
            _textNumber.text = _numbers.ToString();
        }
    }


    [SerializeField] private TextMeshProUGUI _textNumber;
    [SerializeField] private Button _showInfoButton;
    [SerializeField] private TeamEffectInfo _infoObject;

    private void Awake()
    {
        _showInfoButton.onClick.AddListener(ShowMoreInfo);
    }

    private void ShowMoreInfo()
    {
        _infoObject.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Numbers = 0;
    }
}
