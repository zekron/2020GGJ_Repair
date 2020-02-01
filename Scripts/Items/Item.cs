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
        ItemDetector.Add_OnDestroyDetectorTriggered(ChangeSprite);
        _CurSprite.sprite = _ItemSprites[_ItemSprites.Length - 1];
        _NewSprite.sprite = _ItemSprites[_ItemSprites.Length - 2];
    }

    void ChangeSprite(ItemState state)
    {
        _CurSprite.DOFade(0, 1).OnComplete(
            () =>
            {
                _CurSprite.sprite = _NewSprite.sprite;
                _CurSprite.DOFade(1, 0);
            });
        _NewSprite.DOFade(1, 1).OnComplete(
            () =>
            {
                _NewSprite.sprite = _ItemSprites[(int)state];
                _CurSprite.DOFade(0, 0);
            });
    }
}
