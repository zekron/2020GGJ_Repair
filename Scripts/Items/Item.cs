using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite[] _ItemSprites;

    public SpriteRenderer _CurSprite;
    public SpriteRenderer _NewSprite;

    private void Start()
    {
        ItemDetector.instance.Add_OnDestroyDetectorTriggered(ChangeSprite);
        _CurSprite.sprite = _ItemSprites[_ItemSprites.Length - 1];
        _NewSprite.sprite = _ItemSprites[_ItemSprites.Length - 2];
    }

    public void ChangeSprite(ItemState state)
    {
        _CurSprite.DOComplete();
        _NewSprite.DOComplete();

        _NewSprite.sprite = _ItemSprites[(int)state];

        _NewSprite.DOFade(1, 1)
            .OnComplete(
            () =>
            {
                _NewSprite.color = StaticData.ColorFadeOut;
            });
        _CurSprite.DOFade(0, 1)
            .OnComplete(
            () =>
            {
                _CurSprite.sprite = _NewSprite.sprite;
                if (state > ItemState.eStateOne)
                    _NewSprite.sprite = _ItemSprites[(int)state - 1];
                _CurSprite.color = StaticData.ColorFull;
            });
    }
}
