using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ancient : InteractiveObject
{
    [SerializeField]
    public Sprite[][] _KeysAncientSprites;
    public Sprite[] _FinalAncientSprites;

    public void ChangeAncientSprite(int keyIndex, float duration = StaticData.DestroyDuration)
    {
        _CurSprite.DOComplete();
        _NewSprite.DOComplete();

        _NewSprite.sprite = _FinalAncientSprites[keyIndex];

        _NewSprite.DOFade(1, duration)
            .OnComplete(
            () =>
            {
                _NewSprite.color = StaticData.ColorFadeOut;
            });
        _CurSprite.DOFade(0, duration)
            .OnComplete(
            () =>
            {
                _CurSprite.sprite = _NewSprite.sprite;
                _CurSprite.color = StaticData.ColorFull;
            });
    }
}
