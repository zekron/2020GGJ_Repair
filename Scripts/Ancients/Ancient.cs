using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ancient : InteractiveObject
{
    public Sprite[] _FullAncientSprite;

    public void ChangeAncientSprite(int index, float duration = StaticData.DestroyDuration)
    {
        _CurSprite.DOComplete();
        _NewSprite.DOComplete();

        _NewSprite.sprite = _FullAncientSprite[index];

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
