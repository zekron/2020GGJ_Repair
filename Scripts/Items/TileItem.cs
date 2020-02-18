using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileItem : Item
{
    public override void ChangeSprite(eItemState state, float duration = 0.2F)
    {
        _CurSprite.DOComplete();
        _NewSprite.DOComplete();

        _NewSprite.sprite = _ObjectSprites[(int)state];

        _CurSprite.DOFade(0, duration)
            .OnComplete(
            () =>
            {
                _CurSprite.sprite = _NewSprite.sprite;
                if (state > eItemState.eStateOne)
                    _NewSprite.sprite = _ObjectSprites[(int)state - 1];
                _CurSprite.color = StaticData.ColorFull;
            });
    }
}
