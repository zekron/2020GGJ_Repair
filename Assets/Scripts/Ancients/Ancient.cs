using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Ancient : InteractiveObject
{
    public List<BrokenAncientSprite> _BrokenSprites;
    public Sprite[] _UnLockedAncientSprites;
    public Sprite _FinalAncientSprite;

    private int m_KeyCount;

    public override void OnStart()
    {
        m_KeyCount = GetComponent<AncientDetector>()._Keys.Count;
    }

    public void ChangeAncientSprite(int keyIndex, bool setFinal, float duration = StaticData.DestroyDuration)
    {
        _CurSprite.DOComplete();
        _NewSprite.DOComplete();

        if (setFinal)
        {
            _NewSprite.sprite = _FinalAncientSprite;
        }
        else
        {
            m_KeyCount--;
            _NewSprite.sprite = _UnLockedAncientSprites[m_KeyCount == 0 ? _UnLockedAncientSprites.Length - 1 : keyIndex];
            _ObjectSprites = _BrokenSprites[keyIndex]._Sprites;
        }

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
