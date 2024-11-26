using UnityEngine;

public class BackGroundHandler
{
    private SpriteRenderer _backGroundImage;

    public BackGroundHandler(SpriteRenderer bgImage)
    {
        _backGroundImage = bgImage;
    }

    public void SetBackGround(Sprite newSprite)
    {
        _backGroundImage.sprite = newSprite;
    }
}
