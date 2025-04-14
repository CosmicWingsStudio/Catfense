using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LoadingSceneScreen : MonoBehaviour
{
    [SerializeField] private float _fadedawayTime = 3f;
    [SerializeField] private float _baseStepValue = 1f;

    private Image _image;
    private bool GRAActivated = false;

    private void Start()
    {
        _image = GetComponent<Image>();
        StartCoroutine(StartFadingAway());
    }

    private IEnumerator StartFadingAway()
    {
        float timer = 0f;
        float step = _baseStepValue / _fadedawayTime;
        Color newColor = _image.color;

        while (timer < _fadedawayTime)
        {
            timer += Time.deltaTime;
            newColor.a = Mathf.Lerp(newColor.a, 0f, step * timer);
            _image.color = newColor;
            yield return null;
        }

        if (GRAActivated == false)
        {
            GRAActivated = true;
            if (LevelDataProviderFromMenuScene.Instance.IsGRAReady == false)
            {
                YandexGame.GameReadyAPI();
                LevelDataProviderFromMenuScene.Instance.IsGRAReady = true;
                Debug.Log("GRA IS READU");

            }
        }

        Destroy(gameObject);

    }
}
