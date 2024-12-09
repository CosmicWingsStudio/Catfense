using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class GameTipsScreenHandler : MonoBehaviour
{
    [SerializeField] private Button _showScreenButton;

    private Animator _animator;
    private bool IsActivated = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _showScreenButton.onClick.AddListener(HandleTipsScreen);
    }

    private void HandleTipsScreen()
    {
        if (!IsActivated)
        {
            IsActivated = true;

            _animator.SetBool("IsShowing", true);
        }
        else
        {
            IsActivated = false;

            _animator.SetBool("IsShowing", false);
        }
    }
}
