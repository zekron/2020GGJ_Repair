﻿using DG.Tweening;
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
        _CurSprite.sprite = _ItemSprites[_ItemSprites.Length - 1];
        _NewSprite.sprite = _ItemSprites[_ItemSprites.Length - 2];
    }

    public void ChangeSprite(ItemState state,float duration = StaticData.DestroyDuration)
    {
        _CurSprite.DOComplete();
        _NewSprite.DOComplete();

        _NewSprite.sprite = _ItemSprites[(int)state];

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
                if (state > ItemState.eStateOne)
                    _NewSprite.sprite = _ItemSprites[(int)state - 1];
                _CurSprite.color = StaticData.ColorFull;
            });
    }
}