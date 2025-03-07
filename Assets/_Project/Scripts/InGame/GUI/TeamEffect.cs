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
            _effect.UpdateEffect(_numbers);
        }
    }

    [SerializeField] private TextMeshProUGUI _textNumber;
    [SerializeField] private Button _showInfoButton;
    [SerializeField] private TeamEffectInfo _infoObject;

    private TeamEffect_Unit _effect;

    public void EnableEffect()
    {   
        gameObject.SetActive(true);    
    }

    private void Awake()
    {
        _showInfoButton.onClick.AddListener(ShowMoreInfo);
        _effect = GetComponent<TeamEffect_Unit>();
        gameObject.SetActive(false);
    }

    private void ShowMoreInfo()
    {
        _infoObject.gameObject.SetActive(true);
        Vector2 newPos = transform.position;
        newPos.x += 280f;
        _infoObject.transform.position = newPos;
    }

    public void DisableEffect()
    {  
        Numbers = 0;
        _infoObject.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void UpdateEffect(int num)
    { 
        Numbers = num;
        _effect.UpdateEffect(Numbers);
    }
}
