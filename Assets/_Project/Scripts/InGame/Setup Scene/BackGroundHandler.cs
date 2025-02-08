using UnityEngine;

public class BackGroundHandler
{
    private UnityEngine.UI.Image _backGroundImage;

    public BackGroundHandler(UnityEngine.UI.Image bgImage)
    {
        _backGroundImage = bgImage;
    }

    public void SetBackGround(Sprite newSprite)
    {
        _backGroundImage.sprite = newSprite;
    }
}
