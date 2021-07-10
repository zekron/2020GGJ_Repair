using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class AncientItem : Item
{
    public List<BrokenAncientSprite> _BrokenSprites;
    public Sprite[] _UnLockedAncientSprites;
    public Sprite _FinalAncientSprite;

    private int m_KeyCount;

    private void Start()
    {
        m_KeyCount = GetComponent<AncientDetector>()._Keys.Count;
    }

    private void OnValidate()
    {
        _itemStatus.ItemType = GameItemType.Ancient;
        _itemStatus.ItemState = GameItemState.StateFour;
    }

    public void ChangeSprite(int keyIndex, bool setFinal, float duration = StaticData.DestroyDuration)
    {
        _curSprite.DOComplete();
        _newSprite.DOComplete();

        if (setFinal)
        {
            _newSprite.sprite = _FinalAncientSprite;
        }
        else
        {
            m_KeyCount--;
            _newSprite.sprite = _UnLockedAncientSprites[m_KeyCount == 0 ? _UnLockedAncientSprites.Length - 1 : keyIndex];
            _itemSprites = _BrokenSprites[keyIndex]._Sprites;
        }

        _newSprite.DOFade(1, duration)
            .OnComplete(
            () =>
            {
                _newSprite.color = StaticData.ColorFadeOut;
            });
        _curSprite.DOFade(0, duration)
            .OnComplete(
            () =>
            {
                _curSprite.sprite = _newSprite.sprite;
                _curSprite.color = StaticData.ColorFull;
            });
    }
}
